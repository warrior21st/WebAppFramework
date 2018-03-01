using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace WebApp.Authorization
{          
    /// <summary>
    /// 角色权限
    /// </summary>
    [InterfaceGroup("用户/权限管理", "角色权限")]
    public struct ManageRoleAuthorities : IManageInterface
    {
        /// <summary>
        /// 读取
        /// </summary>
        [Description("读取")]
        public const string READ = "read";

        /// <summary>
        /// 写入
        /// </summary>
        [Description("写入")]
        public const string WRITE = "write";
    }

    [InterfaceGroup("业务用户管理", "用户列表")]
    public struct ManageAppUser : IManageInterface
    {
        /// <summary>
        /// 读取
        /// </summary>
        [Description("读取")]
        public const string READ = "read";
    }

    [InterfaceGroup("业务用户管理", "登录日志")]
    public struct ManageAppLoginLog : IManageInterface
    {
        /// <summary>
        /// 读取
        /// </summary>
        [Description("读取")]
        public const string READ = "read";
    }

    /// <summary>
    /// 用户管理
    /// </summary>
    [InterfaceGroup("用户/权限管理", "用户管理")]
    public struct ManageUser : IManageInterface
    {
        /// <summary>
        /// 读取
        /// </summary>
        [Description("读取")]
        public const string READ = "read";

        /// <summary>
        /// 写入
        /// </summary>
        [Description("写入")]
        public const string WRITE = "write";

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        public const string DELETE = "delete";

        /// <summary>
        /// 设置用户权限
        /// </summary>
        [Description("设置用户权限")]
        public const string SET_USER_AUTHORITY = "set_user_authority";
    }

    public interface IManageInterface
    {

    }

    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class InterfaceGroupAttribute : Attribute
    {
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }

        public InterfaceGroupAttribute(string group, string desc)
        {
            GroupName = group;
            Description = desc;
        }
    }
}
