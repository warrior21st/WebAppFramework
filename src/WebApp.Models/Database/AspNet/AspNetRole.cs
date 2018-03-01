using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApp.Models.Database.Base;

namespace WebApp.Models.Database.AspNet
{
    public class AspNetRole : BaseUpdateEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary> 
        [MaxLength(50)]
        public string AliasName { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        [MaxLength(36)]
        public string AuthorityId { get; set; }
    }

    /// <summary>
    /// 角色类型
    /// </summary>
    public enum RoleTypes
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("系统管理员")]
        Admin = 0,

        /// <summary>
        /// 主管
        /// </summary>
        [Description("主管")]
        Manager = 1,

        /// <summary>
        /// 运营者
        /// </summary>
        [Description("运营者")]
        Operational = 2,

        /// <summary>
        /// 普通用户
        /// </summary>
        [Description("用户")]
        User = 3
    }
}
