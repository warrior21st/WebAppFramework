using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApp.Models.Database.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Database
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("AppUser")]
    public class AppUser : BaseUpdateEntity
    {
        [MaxLength(255)]
        public string UserName { get; set; }

        [MaxLength(255)]
        public string PasswordHash { get; set; }
        
        /// <summary>
        /// 别名
        /// </summary>
        [MaxLength(255)]
        public string AliasName { get; set; }

        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string MailAddress { get; set; }

        [MaxLength(255)]
        public string IdCardNumber { get; set; }

        [MaxLength(255)]
        public string IdCardImageUri { get; set; }

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
