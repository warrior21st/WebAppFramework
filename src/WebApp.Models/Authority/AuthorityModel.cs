using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Models.Authority
{
    /// <summary>
    /// 权限模型
    /// </summary>
    public class AuthorityModel
    {
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 接口列表
        /// </summary>
        public List<InterfaceModel> Interfaces { get; set; }

        public AuthorityModel()
        {
            Interfaces = new List<InterfaceModel>();
        }
    }
}
