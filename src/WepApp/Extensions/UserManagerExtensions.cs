using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Helpers;
using WebApp.Models.Database.AspNet;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// 用户管理类扩展类
    /// </summary>
    public static class UserManagerExtensions
    {
        /// <summary>
        /// 获取更小角色的用户列表
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<List<AspNetUser>> GetLesserRoleUsersAsync(this UserManager<AspNetUser> userManager, AspNetUser user)
        {
            var list = new List<AspNetUser>();
            var roles = await userManager.GetRolesAsync(user);
            var userRole = roles.First();
            var lesserRoles = RoleHelper.GetLesserRoles(Enum.Parse<RoleTypes>(userRole));
            foreach(var r in lesserRoles)
            {
                list.AddRange(await userManager.GetUsersInRoleAsync(r.ToString()));
            }           

            return list;
        }

        /// <summary>
        /// 根据用户id列表获取role列表
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="context"></param>
        /// <param name="uids"></param>
        /// <returns></returns>
        public static async Task<List<RoleTypes>> GetRolesByUserIdsAsync(this UserManager<AspNetUser> userManager, AppDbContext context, List<string> uids)
        {
            var list = new List<RoleTypes>();
            var sql = $"SELECT b.* FROM aspnetroles b,aspnetuserroles c WHERE b.Id=c.RoleId AND c.UserId IN ({uids.GetSqlConditionString()})";
            var roles = await context.QueryListBySqlAsync<AspNetRole>(sql);
            foreach(var r in roles)
            {
                list.Add(Enum.Parse<RoleTypes>(r.Name));
            }

            return list;
        }
    }
}
