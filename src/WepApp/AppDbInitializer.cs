using WebApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebApp.Helpers;
using System.Reflection;
using System.ComponentModel;
using WebApp.Models.Database.AspNet;
using WebApp.Authorization;
using CommonHelpers.Algorithm;
using Microsoft.EntityFrameworkCore;

#pragma warning disable 1591
namespace WebApp
{
    /// <summary>
    /// 数据库初始化类
    /// </summary>
    public static class AppDbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var contextOptions = serviceProvider.GetService<DbContextOptions<AppDbContext>>();
                using (var context = new AppDbContext(contextOptions))
                {
                    await CreateRoles(serviceProvider, context);

                    context.Database.EnsureCreated();

                    Console.WriteLine("default manager user was initialized...");

                    await AuthorityDbInitializer.InitInterfaceOperations(serviceProvider, context);

                    Console.WriteLine("all manager interface operation was initialized...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"default manager user initialize exception:{ex.Message}");
            }


            //context.Database.Migrate();

            //context.SaveChanges();
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="serviceProvider"></param>
        private static async Task CreateRoles(IServiceProvider serviceProvider, AppDbContext context)
        {

            var configuration = serviceProvider.GetService<IConfiguration>();

            var roles = RoleHelper.GetBuiltInRoles();
            var existRoles = context.Roles.ToList();
            foreach (var role in roles)
            {
                if (!existRoles.Any(x => x.Name == role.Name))
                {
                    role.CreateTime = DateTime.UtcNow;
                    role.LastUpdate = role.CreateTime;
                    context.Roles.Add(role);
                }
            }
            var password = configuration["AdminAccount:Password"];
            var poweruser = new AspNetUser
            {
                UserName = configuration["AdminAccount:UserName"],
                PasswordHash = Hash.GetMd5(password),
                CreateTime = DateTime.UtcNow,
                LastUpdate = DateTime.UtcNow,
                AuthorityId = Guid.NewGuid().ToString("N")
            };

            if (context.Users.Count(x => x.UserName == poweruser.UserName) == 0)
                context.Users.Add(poweruser);

            await context.SaveChangesAsync();
        }
    }
}
