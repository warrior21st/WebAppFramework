using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Models.Database.AspNet
{
    public class AspNetUser : IdentityUser<string>
    {
        public AspNetUser()
        {
            Id = Guid.NewGuid().ToString();
            AuthorityId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 登陆强制要求手机验证
        /// </summary>
        public bool LoginVerifyPhoneNumber { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 谷歌认证器secretkey
        /// </summary>
        [MaxLength(127)]
        public string GoogleAuthenticatorSecretKey { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        [MaxLength(36)]
        public string AuthorityId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
