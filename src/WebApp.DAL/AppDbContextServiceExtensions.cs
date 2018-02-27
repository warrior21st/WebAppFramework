﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.DAL;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppDbContextServiceExtensions
    {
        /// <summary>
        /// 添加数据访问层，使用efcore
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDALByEfCore(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //添加数据连接
            string connectionString = configuration.GetSection("AppSettings")["ConnectionString"];

            //Add framework services.
            //Pomelo.EntityFrameworkCore.MySql
            services.AddEntityFrameworkMySql()
                .AddDbContext<AppDbContext>(options => options.UseMySql(connectionString));
            services.AddTransient((x) =>
            {
                var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                        .UseMySql(connectionString)
                        .Options;

                return contextOptions;
            });

            return services;
        }
    }
}
