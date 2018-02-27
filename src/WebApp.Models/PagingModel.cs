using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace WebApp.Models
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PagingModel
    {
        /// <summary>
        /// 可选每页行数
        /// </summary>
        public static int[] PageSizeArray = new int[6] { 20, 50, 100, 200, 300, 500 };

        /// <summary>
        /// 最小每页行数
        /// </summary>
        public const int MIN_PAGE_SIZE = 20;

        /// <summary>
        /// 每页行数100
        /// </summary>
        public const int PAGE_SIZE_100 = 100;

        /// <summary>
        /// 最大每页行数
        /// </summary>
        public const int MAX_PAGE_SIZE = 500;

        private int _pageIndex { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                if (value < 1)
                    _pageIndex = 1;
                else
                    _pageIndex = value;
            }
        }


        private int _pageSize { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value < MIN_PAGE_SIZE)
                    _pageSize = MIN_PAGE_SIZE;
                else if (value > MAX_PAGE_SIZE)
                    _pageSize = MAX_PAGE_SIZE;
                else
                    _pageSize = value;
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public long PageCount
        {
            get
            {
                return Total % PageSize > 0 ? Total / PageSize + 1 : Total / PageSize;
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public Dictionary<string, string> QueryParams { get; set; }

        /// <summary>
        /// 数据库开始索引
        /// </summary>
        public int Start
        {
            get
            {
                return (_pageIndex - 1) * PageSize;
            }
        }

        /// <summary>
        /// 数据库查询行数
        /// </summary>
        public int Limit
        {
            get
            {
                return _pageSize;
            }
        }

        public PagingModel()
        {
            this.QueryParams = new Dictionary<string, string>();
        }
    }
}
