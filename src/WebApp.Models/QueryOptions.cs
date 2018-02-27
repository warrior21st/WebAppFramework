using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace WebApp.Models
{
    /// <summary>
    /// 数据库查询通用参数
    /// </summary>
    public class QueryOptions
    {
        /// <summary>
        /// 升序
        /// </summary>
        public const string ORDER_ASC = "ASC";

        /// <summary>
        /// 降序
        /// </summary>
        public const string ORDER_DESC = "DESC";

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortColumn { get; set; }

        private string _order;

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order
        {
            get
            {
                return _order;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.ToLower() == ORDER_DESC.ToLower())
                    _order = ORDER_DESC;
                else
                    _order = ORDER_ASC;
            }
        }

        /// <summary>
        /// 开始索引
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int Limit { get; set; }

        public QueryOptions(string sortColumn, string order, int start, int limit)
        {
            this.SortColumn = sortColumn;
            this.Order = order;
            this.Start = start;
            this.Limit = limit;
        }
    }
}
