using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 基础扩展
    /// </summary>
    public static class BasicExtensions
    {
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool IsNull(this string content)
        {
            return string.IsNullOrWhiteSpace(content);
        }

        /// <summary>
        /// 过滤乱码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FilterInvalidChar(this string content)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(content))
            {
                content = content.Trim();
                var arr = new UnicodeCategory[] { UnicodeCategory.OtherSymbol, UnicodeCategory.Surrogate, UnicodeCategory.Control };
                foreach (var c in content)
                {
                    if (!arr.Contains(CharUnicodeInfo.GetUnicodeCategory(c)))
                        result += c;
                }
            }

            return result;
        }

        /// <summary>
        /// 移除非charset包含的字符
        /// </summary>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string RemoveNotCharsetChar(this string charset)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(charset))
            {
                var symbolArr = new char[] { '-' };
                var ucArr = new UnicodeCategory[] { UnicodeCategory.LowercaseLetter, UnicodeCategory.UppercaseLetter, UnicodeCategory.DecimalDigitNumber };
                foreach (var c in charset.Trim())
                {
                    var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (symbolArr.Contains(c) || ucArr.Contains(uc))
                        result += c;
                }
            }

            return result;
        }

        /// <summary>
        /// 忽略大小写比对2个字符串是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CompareIgnoreCase(this string a, string b)
        {
            return a.ToLower() == b.ToLower();
        }

        /// <summary>
        /// 判断字符串是否包含数组中的任一元素
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool ContainsArray(this string sourceStr, string[] arr)
        {
            var b = false;
            if (!string.IsNullOrWhiteSpace(sourceStr) && arr != null && arr.Length > 0)
            {
                foreach (var s in arr)
                {
                    if (sourceStr.Contains(s))
                    {
                        b = true;
                        break;
                    }
                }
            }

            return b;
        }

        /// <summary>
        /// 是否是有效的时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsValidDatetime(this DateTime dt)
        {
            return dt != null && dt != default(DateTime);
        }

        /// <summary>
        /// biginteger字符串乘法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string MultiplicationBigIntegerString(this string a, string b)
        {
            return (BigInteger.Parse(a) * BigInteger.Parse(b)).ToString();
        }

        /// <summary>
        /// bigint字符串加法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BigInteger PlusBitInteger(this string a, string b)
        {
            return BigInteger.Parse(a) + BigInteger.Parse(b);
        }

        /// <summary>
        /// 转换为biginteger
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static BigInteger ToBigInteger(this string a)
        {
            return string.IsNullOrWhiteSpace(a) ? 0 : BigInteger.Parse(a);
            //BigInteger r = 0;
            //if (BigInteger.TryParse(a, out var b))
            //    r = b;

            //return r;
        }

        /// <summary>
        /// 判断字符是否是十进制数
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsDecimalDigitNumber(this char c)
        {
            var charCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            return charCategory == UnicodeCategory.DecimalDigitNumber;
        }

        /// <summary>
        /// 忽略大小写检查是否包含字符串
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string a, string b)
        {
            return a.ToLower().Contains(b.ToLower());
        }

        /// <summary>
        /// 去掉尾部的0
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static decimal TrimEndDecimalZero(this decimal number)
        {
            decimal result = 0M;
            if (number > 0)
            {
                result = number;
                var str = number.ToString();
                if (str.EndsWith("0") && str.Contains("."))
                {
                    for (int i = str.Length - 1; i >= 0; i--)
                    {
                        if (str[i] != '0')
                        {
                            str = str.Substring(0, i + 1);
                            break;
                        }
                    }
                    result = decimal.Parse(str);
                }
            }

            return result;
        }

        /// <summary>
        /// 转换为标准格式字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isUtc"></param>
        /// <returns></returns>
        public static string ToStandardDatetimeString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转为标准日期字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isUtc"></param>
        /// <returns></returns>
        public static string ToStandardDateString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 转为本地日期时间字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timeZone">时区，默认8</param>
        /// <returns></returns>
        public static string ToLocalDateTimeString(this DateTime dt,int timeZone = 8)
        {
            var d = dt;
            if (timeZone > 0)
                d = d.AddHours(timeZone);
            return d.ToStandardDatetimeString();
        }

        /// <summary>
        /// 转为本地日期字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timeZone">时区，默认8</param>
        /// <returns></returns>
        public static string ToLocalDateString(this DateTime dt,int timeZone = 8)
        {
            var d = dt;
            if (timeZone > 0)
                d = d.AddHours(timeZone);
            return d.ToStandardDateString();
        }

        /// <summary>
        /// 去除乱码
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        //public static string FilterMessyCode(this string str)
        //{
        //    var result = string.Empty;
        //    if (!string.IsNullOrWhiteSpace(str))
        //    {                
        //        var validArr = new UnicodeCategory[] { UnicodeCategory.DecimalDigitNumber, UnicodeCategory.LowercaseLetter, UnicodeCategory.UppercaseLetter };
        //        foreach (var c in str)
        //        {
        //            if (validArr.Contains(CharUnicodeInfo.GetUnicodeCategory(c)))
        //                result += c;
        //        }
        //    }

        //    return string.IsNullOrWhiteSpace(result) ? str : result;
        //}
    }
}
