using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Models.Database.Base;

namespace WebApp.Models.Database.AspNet
{
    [Table("AspNetUser")]
    public class AspNetUser:BaseUpdateEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 谷歌认证器secretkey
        /// </summary>
        [MaxLength(255)]
        public string GoogleAuthSecretKey { get; set; }

        /// <summary>
        /// 是否开启谷歌验证
        /// </summary>
        public bool GooleAuthEnabled { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        [MaxLength(36)]
        public string AuthorityId { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
    }
}
