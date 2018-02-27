using CommonHelpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    /// <summary>
    /// 分页模型扩展类
    /// </summary>
    public static class PagingModelExistensions
    {
        /// <summary>
        /// 获取第一页地址
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public static string GetFirstPageUrl(this PagingModel paging)
        {
            return GetPageUrl(paging, 1, paging.PageSize);
        }

        /// <summary>
        /// 获取上一页地址
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public static string GetPreviousPageUrl(this PagingModel paging)
        {
            return GetPageUrl(paging, paging.PageIndex - 1, paging.PageSize);
        }

        /// <summary>
        /// 获取下一页地址
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public static string GetNextPageUrl(this PagingModel paging)
        {
            return GetPageUrl(paging, paging.PageIndex + 1, paging.PageSize);
        }

        /// <summary>
        /// 获取指定页码的url
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static string GetPageUrlByPageIndex(this PagingModel paging, int pageIndex)
        {
            return GetPageUrl(paging, pageIndex, paging.PageSize);
        }

        /// <summary>
        /// 获取页面url
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static string GetPageUrl(this PagingModel paging, int pageIndex, int pageSize)
        {
            var result = paging.RequestUrl;
            var queryString = "";
            if (paging.QueryParams.Count > 0)
            {

                foreach (var kvp in paging.QueryParams)
                {
                    if (!string.IsNullOrWhiteSpace(kvp.Value) && !kvp.Key.CompareIgnoreCase("pageIndex") && !kvp.Key.CompareIgnoreCase("pagesize"))
                    {
                        queryString += $"{kvp.Key}={kvp.Value}&";
                    }
                }
                queryString = queryString.TrimEnd('&');
            }
            if (!string.IsNullOrWhiteSpace(queryString))
                result = $"{result}?{queryString}";
            if (pageIndex > 1)
                result = UrlHelper.AddQueryParamToUrl(result, "pageIndex", pageIndex.ToString());
            if (pageSize != PagingModel.MIN_PAGE_SIZE)
                result = UrlHelper.AddQueryParamToUrl(result, "pageIndex", pageSize.ToString());

            return result;
        }
    }
}
