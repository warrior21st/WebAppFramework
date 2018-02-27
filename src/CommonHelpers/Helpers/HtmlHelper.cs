using System;
using System.Collections.Generic;
using System.Text;

namespace CommonHelpers.Helpers
{
    /// <summary>
    /// html帮助类
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetString(string str)
        {
            var result = string.IsNullOrWhiteSpace(str) ? null : str.Trim();
            if (!string.IsNullOrWhiteSpace(result))
            {
                result = result.Replace("\t", "").Replace("\n", "").Replace("\r", "");
            }

            return result;
        }
    }
}
