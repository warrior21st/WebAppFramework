using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Models.ClientInfo
{
    /// <summary>
    /// 客户端信息模型
    /// </summary>
    public class ClientInfoModel
    {
        public string IP { get; set; }

        public string UserAgent { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 州/省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
    }
}
