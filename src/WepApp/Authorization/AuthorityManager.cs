using CommonHelpers.Helpers;
using WebApp.DAL;
using WebApp.Models.Authorization;
using WebApp.Models.Database;
using WebApp.Models.Database.AspNet;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace WebApp.Authorization
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public class AuthorityManager
    {
        private readonly AppDbContext _context;

        public const string GET_ALL_OPERATION_SQL = @"SELECT b.Id,a.Name AS InterfaceName,b.Name AS OperationName 
                            FROM(SELECT Id, NAME FROM `interfaceoperation` WHERE ParentName IS NULL) a,interfaceoperation b WHERE a.Name = b.ParentName";

        public AuthorityManager(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 将用户权限加载到缓存
        /// </summary>
        /// <param name="user"></param>
        public async Task LoadUserAuthority(AspNetUser user)
        {
            await GetUserAuthoritiesAsync(user);
        }

        /// <summary>
        /// 根据权限id移除缓存
        /// </summary>
        /// <param name="authorityId"></param>
        public void RemoveAuthorityCacheByAuthorityId(string authorityId)
        {
            var role = _context.Roles.SingleOrDefault(x => x.AuthorityId == authorityId);
            AspNetUser user = null;
            if (role == null)
                user = _context.Users.Single(x => x.AuthorityId == authorityId);
            if (role != null)
                RemoveRoleAuthorityCache(role);
            else
                RemoveUserAuthorityCache(user);
        }

        /// <summary>
        /// 移除角色下所有用户的权限缓存
        /// </summary>
        /// <param name="role"></param>
        public void RemoveRoleAuthorityCache(AspNetRole role)
        {
            var uids = _context.UserRoles.Where(x => x.RoleId == role.Id).Select(x => x.UserId).ToList();
            var users = _context.Users.Where(x => uids.Contains(x.Id)).ToList();
            foreach (var user in users)
            {
                RemoveUserAuthorityCache(user);
            }
        }

        /// <summary>
        /// 移除用户权限缓存
        /// </summary>
        /// <param name="user"></param>
        public void RemoveUserAuthorityCache(AspNetUser user)
        {
            var key = $"{user.Id}_UserAuthorities";
            MemoryCacheHelper.RemoveCache(key);
        }

        /// <summary>
        /// 根据用户名获取用户接口权限列表
        /// </summary>
        /// <param name="uname"></param>
        /// <returns></returns>
        public async Task<List<InterfaceOperationModel>> GetUserAuthoritiesByUserNameAsync(string uname)
        {
            var user = _context.Users.Single(x => x.UserName == uname);
            return await GetUserAuthoritiesAsync(user);
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<InterfaceOperationModel>> GetUserAuthoritiesAsync(AspNetUser user)
        {
            var key = $"{user.Id}_UserAuthorities";

            if (!MemoryCacheHelper.Exists(key))
            {
                var list = new List<InterfaceOperationModel>();

                var b = (await _context.QueryNumberBySqlAsync($"SELECT COUNT(b.Id) FROM AspNetRole a,AspNetUserRole b WHERE a.Id=b.RoleId AND b.UserId={user.Id} AND a.Name='{nameof(RoleTypes.Admin)}'")) > 0;
                string sql = GET_ALL_OPERATION_SQL;
                if (!b)
                {
                    sql = $@"SELECT t2.InterfaceName,t2.OperationName FROM ({GET_ALL_OPERATION_SQL}) t2,DDomainAuthority t3,`aspnetusers` t4 WHERE t3.`OperationId`=t2.`Id`
                            AND t4.`Id`= '{user.Id}' AND(t3.`AuthorityId`= t4.`AuthorityId` OR t3.`AuthorityId` IN(SELECT a.AuthorityId FROM `aspnetroles` a,`aspnetuserroles` b WHERE a.`Id`= b.`RoleId` AND b.`UserId`= '{user.Id}'))";
                }

                list = await _context.QueryListBySqlAsync<InterfaceOperationModel>(sql);

                MemoryCacheHelper.SetCache(key, list);
            }

            return MemoryCacheHelper.GetCache<List<InterfaceOperationModel>>(key);
        }
    }
}
