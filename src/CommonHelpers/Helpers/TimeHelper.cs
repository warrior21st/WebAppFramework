using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CommonHelpers.Helpers
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// 根据时间戳（1970.1.1到现在的秒数）获取时间
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeByNumber(double number)
        {
            var dt = DateTime.Parse("1970-01-01 00:00:00.000");
            return dt.AddSeconds(number);
        }
    }
}
