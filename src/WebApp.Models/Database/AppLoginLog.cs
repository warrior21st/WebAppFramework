using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Models.Database.Base;

namespace WebApp.Models.Database
{
    /// <summary>
    /// 业务用户登录日志
    /// </summary>
    [Table("AppLoginLog")]
    public class AppLoginLog:BaseEntity
    {
        /// <summary>
        /// 登陆用户id
        /// </summary>
        public int Uid { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        [MaxLength(255)]
        public string Ip { get; set; }

        /// <summary>
        /// useragent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [MaxLength(255)]
        public string Country { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [MaxLength(255)]
        public string City { get; set; }
    }
}
