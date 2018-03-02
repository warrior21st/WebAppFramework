using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CommonHelpers.Helpers
{
    /// <summary>
    /// 服务器帮助类
    /// </summary>
    public class ServerHelper
    {
        /// <summary>
        /// 获取程序根目录
        /// </summary>
        /// <returns></returns>
        public static string GetRootDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP(HttpContext context)
        {
            string result = GetInfoFromRequestHeader(context.Request.Headers, "X-Forwarded-For");
            if (string.IsNullOrWhiteSpace(result))
                result = context.Connection.RemoteIpAddress.ToString();

            return result;
        }

        /// <summary>
        /// 获取客户端浏览器信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientUserAgent(HttpContext context)
        {
            string result = GetInfoFromRequestHeader(context.Request.Headers, "User-Agent");
            if (string.IsNullOrWhiteSpace(result))
                result = GetInfoFromRequestHeader(context.Request.Headers, "UserAgent");

            return result;
        }

        /// <summary>
        /// 获取客户端页面地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientReferer(HttpContext context)
        {
            string result = GetInfoFromRequestHeader(context.Request.Headers, "Referer");

            return result;
        }

        /// <summary>
        /// 从请求头里获取信息
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetInfoFromRequestHeader(IHeaderDictionary headers, string key)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(headers[key].FirstOrDefault()))
                result = headers[key].FirstOrDefault();

            return result;
        }

        /// <summary>
        /// 根据相对路径获取绝对路径
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string MapPath(string uri)
        {
            var path = GetRootDirectory();
            string[] arr = uri.Split('/');
            foreach (var s in arr)
            {
                if (!string.IsNullOrWhiteSpace(s))
                    path = Path.Combine(path, s);
            }

            return path;
        }
    }
}
