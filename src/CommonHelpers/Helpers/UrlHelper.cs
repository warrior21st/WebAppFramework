using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonHelpers.Helpers
{
    /// <summary>
    /// url帮助类
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// 添加参数到url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AddQueryParamToUrl(string url, string key, string value)
        {
            var symbol = url.Contains("?") ? "&" : "?";
            if (!url.Contains($"{symbol}{key}={value}"))
            {
                url += $"{symbol}{key}={value}";
            }

            return url;
        }

        /// <summary>
        /// 获取url的主机
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHost(string url)
        {
            var uri = new Uri(url, UriKind.Absolute);
            return uri.Host;
        }

        /// <summary>
        /// 是否是https请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsHttps(string url)
        {
            var b = url.ToLower().StartsWith("https");
            if (!b)
            {
                var uri = new Uri(url, UriKind.Absolute);
                b = uri.Scheme.ToLower().StartsWith("https");
            }

            return b;
        }

        /// <summary>
        /// 获取url的顶级域名
        /// </summary>
        /// <param name="url"></param>
        /// <param name="allDomainSuffixs"></param>
        /// <returns></returns>
        public static string GetTopDomain(string url, string[] allDomainSuffixs)
        {
            var result = string.Empty;
            if (!string.IsNullOrWhiteSpace(url))
            {
                var uri = new Uri(url, UriKind.Absolute);
                result = uri.Host;

                if (result.Contains(":"))
                    result = result.Split(':')[0];
                var resultLower = result.ToLower();
                var resultList = new List<string>();
                var resultTemp = string.Empty;
                foreach (var suf in allDomainSuffixs)
                {
                    if (resultLower.EndsWith(suf))
                    {
                        resultTemp = result;
                        var suffix = resultTemp.Substring(resultTemp.Length - suf.Length, suf.Length);
                        resultTemp = resultTemp.Replace(suffix, "");
                        if (resultTemp.EndsWith("."))
                            resultTemp = resultTemp.TrimEnd('.');
                        var arr = resultTemp.Split('.');
                        resultTemp = $"{arr[arr.Length - 1]}.{suffix}";

                        resultList.Add(resultTemp);
                    }
                }

                if (resultList.Count > 0)
                {
                    result = resultList[0];
                    foreach (var domain in resultList)
                    {
                        if (result.Length < domain.Length)
                            result = domain;
                    }
                }
            }

            return result;
        }
    }
}
