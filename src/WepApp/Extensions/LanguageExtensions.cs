using CommonHelpers.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 语言扩展
    /// </summary>
    public static class LanguageExtensions
    {
        private const string LANGUAGE_COOKIE_NAME = "LANGUAGE";

        /// <summary>
        /// 设置当前语言
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="language"></param>
        /// <param name="expiresDays"></param>
        public static void SetCurrentLanguageByCookie(this HttpContext httpContext, string language, int expiresDays = 30)
        {
            httpContext.Response.Cookies.Append(LANGUAGE_COOKIE_NAME, language, new CookieOptions() { Expires = DateTime.Now.AddDays(expiresDays), HttpOnly = false, Path = "/" });
        }

        /// <summary>
        /// 获取当前语言
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetCurrentLanguageFromCookie(this HttpContext httpContext)
        {
            string language = string.Empty;
            if (httpContext.Request.Cookies.TryGetValue(LANGUAGE_COOKIE_NAME, out var lang))
                language = lang;

            return language;
        }
    }
}
