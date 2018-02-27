using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    /// <summary>
    /// list扩展方法
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 获取sql条件字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetSqlConditionString(this List<int> list)
        {
            var condition = string.Join(",", list);

            return condition;
        }

        /// <summary>
        /// 获取sql条件字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetSqlConditionString(this List<string> list)
        {
            var condition = "";
            foreach(var s in list)
            {
                condition += $"'{s}',";
            }
            condition = condition.TrimEnd(',');

            return condition;
        }
    }
}
