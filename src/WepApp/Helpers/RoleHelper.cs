using CommonHelpers.Helpers;
using WebApp.Models.Database;
using WebApp.Models.Database.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    /// <summary>
    /// 账号帮助类
    /// </summary>
    public static class RoleHelper
    {
        /// <summary>
        /// 获取内置的角色
        /// </summary>
        /// <returns></returns>
        static public List<AspNetRole> GetBuiltInRoles()
        {
            List<AspNetRole> roles = new List<AspNetRole>();

            roles.Add(new AspNetRole() { Name = nameof(RoleTypes.Admin), AliasName =EnumHelper.GetEnumDescription(RoleTypes.Admin), AuthorityId = Guid.NewGuid().ToString("N") });
            roles.Add(new AspNetRole() { Name = nameof(RoleTypes.Manager), AliasName = EnumHelper.GetEnumDescription(RoleTypes.Manager), AuthorityId = Guid.NewGuid().ToString("N") });
            roles.Add(new AspNetRole() { Name = nameof(RoleTypes.Operational), AliasName = EnumHelper.GetEnumDescription(RoleTypes.Operational), AuthorityId = Guid.NewGuid().ToString("N") });
            roles.Add(new AspNetRole() { Name = nameof(RoleTypes.User), AliasName = EnumHelper.GetEnumDescription(RoleTypes.User), AuthorityId = Guid.NewGuid().ToString("N") });

            return roles;
        }

        /// <summary>
        /// 查找角色的别名
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        static public String FindRoleAliasName(string roleName)
        {
            var roles = GetBuiltInRoles();

            return roles.Find(x => x.Name == roleName).AliasName;
        }

        /// <summary>
        /// 返回默认的权限
        /// </summary>
        /// <returns></returns>
        static public Dictionary<RoleTypes, string[]> GetBuiltInRolePolicies()
        {
            Dictionary<RoleTypes, string[]> policy = new Dictionary<RoleTypes, string[]>();

            policy.Add(RoleTypes.Admin, new string[] { nameof(RoleTypes.Admin) });
            policy.Add(RoleTypes.Manager, new string[] { nameof(RoleTypes.Manager) });
            policy.Add(RoleTypes.Operational, new string[] { nameof(RoleTypes.Operational) });
            policy.Add(RoleTypes.User, new string[] { nameof(RoleTypes.User) });

            return policy;
        }

        /// <summary>
        /// 获取更小的角色list
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static List<RoleTypes> GetLesserRoles(RoleTypes role)
        {
            var list = new List<RoleTypes>();
            var roleInt = (int)role;
            var roles = Enum.GetValues(typeof(RoleTypes));
            foreach(var r in roles)
            {
                if ((int)r > roleInt)
                    list.Add((RoleTypes)r);
            }

            return list;
        }
    }
}
