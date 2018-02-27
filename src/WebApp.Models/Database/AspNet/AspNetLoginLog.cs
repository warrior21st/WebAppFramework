using WebApp.Models.Database.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Database.AspNet
{
    [Table("AspNetLoginLog")]
    public class AspNetLoginLog : BaseEntity
    {
        /// <summary>
        /// 登录者用户id
        /// </summary>
        [MaxLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// 登录者ip
        /// </summary>
        [MaxLength(50)]
        public string ClientIp { get; set; }

        /// <summary>
        /// 登录者所在国家
        /// </summary>
        [MaxLength(255)]
        public string ClientCountry { get; set; }

        /// <summary>
        /// 登录者所在省
        /// </summary>
        [MaxLength(255)]
        public string ClientProvince { get; set; }

        /// <summary>
        /// 登录者所在城市
        /// </summary>
        [MaxLength(255)]
        public string ClientCity { get; set; }

        /// <summary>
        /// 登录者useragent
        /// </summary>
        public string UserAgent { get; set; }
    }
}
