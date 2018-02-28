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

        public LocationModel Location { get; set; }
    }
}
