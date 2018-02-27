using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace WebApp.Models.Database
{
    /// <summary>
    /// 实体信息
    /// </summary>
    public class EntityInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字段名列表
        /// </summary>
        public List<string> Columns { get; set; }

        /// <summary>
        /// 属性列表
        /// </summary>
        public PropertyInfo[] Properties { get; set; }
    }
}
