using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Models.Authorization
{
    /// <summary>
    /// 接口模型
    /// </summary>
    public class InterfaceModel
    {
        /// <summary>
        /// 接口名
        /// </summary>
        public string InterfaceName { get; set; }

        /// <summary>
        /// 接口描述
        /// </summary>
        public string InterfaceDescription { get; set; }

        /// <summary>
        /// 操作列表
        /// </summary>
        public List<OperationModel> Operations { get; set; }

        public InterfaceModel()
        {
            Operations = new List<OperationModel>();
        }
    }
}
