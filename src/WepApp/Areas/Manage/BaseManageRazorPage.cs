using DDomain.Caching.Language;
using DDomain.Models;
using DDomain.Models.Language;
using Microsoft.AspNetCore.Mvc.Razor;
using NGettext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDomain.Areas.Manage
{
    public abstract class BaseManageRazorPage<TModel> : RazorPage<TModel>
    {
        /// <summary>
        /// 获取查询字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetQueryParam(string key)
        {
            var result = "";
            if (ViewData["paging"] != null)
            {
                var queryParams = ((PagingModel)ViewData["paging"]).QueryParams;
                result = queryParams.ContainsKey(key) ? queryParams[key] : "";
            }

            return result;
        }

        private string[] _userInterfaceAuthorities;

        /// <summary>
        /// 用户权限
        /// </summary>
        protected string[] UserInterfaceAuthorities
        {
            get
            {
                if (_userInterfaceAuthorities == null)
                    _userInterfaceAuthorities = ViewData["UserInterfaceAuthorities"] == null ? new string[] { } : (string[])ViewData["UserInterfaceAuthorities"];

                return _userInterfaceAuthorities;
            }
        }

        protected string Search
        {
            get
            {
                return GetQueryParam("search");
            }
        }

        protected string Sort
        {
            get
            {
                return GetQueryParam("sort");
            }
        }

        protected string Order
        {
            get
            {
                return GetQueryParam("order");
            }
        }
    }
}
