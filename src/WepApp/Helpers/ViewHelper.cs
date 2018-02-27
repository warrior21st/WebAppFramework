using CommonHelpers.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using WebApp.Models.Authority;

namespace WebApp.Helpers
{
    /// <summary>
    /// 视图工具类
    /// </summary>
    public static class ViewHelper
    {
        #region 手机号

        /// <summary>
        /// 隐藏手机号中间4位
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string HideMobilePhone(string mobile)
        {
            var result = string.Empty;
            if (!string.IsNullOrWhiteSpace(mobile))
            {
                for (int i = 0; i < mobile.Length; i++)
                {
                    if (i >= 3 && i <= 6)
                        result += "*";
                    else
                        result += mobile[i];
                }
            }

            return result;
        }

        #endregion

        /// <summary>
        /// 标准表格css类
        /// </summary>
        public static string StandardTableClass
        {
            get
            {
                return "table table-striped table-bordered table-hover dataTable";
            }
        }

        /// <summary>
        /// 获取表格头css类
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static string GetTableTheadClass(string columnName, Dictionary<string, string> para)
        {
            var result = "sorting";
            if (para != null && para.Count > 0 && para.ContainsKey("sort") && columnName == para["sort"])
            {
                result += $"_{para["order"].ToLower()}";
            }

            return result;
        }

        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="a"></param>
        /// <param name="all"></param>
        /// <param name="keepDecimal"></param>
        /// <returns></returns>
        public static float ComputePercent(float a, float all, int keepDecimal = 2)
        {
            float result = 0;
            if (all > 0)
            {
                result = a / all * 100;
                var str = result.ToString();
                if (str.Contains("."))
                {
                    var arr = str.Split('.');
                    if (keepDecimal > 0)
                    {
                        str = arr[0] + ".";
                        for (int i = 0; i < arr[1].Length && i < keepDecimal; i++)
                        {
                            str += arr[1][i];
                        }
                        result = float.Parse(str);
                    }
                    else
                        result = float.Parse(Math.Round(decimal.Parse(str)).ToString());
                }
            }

            return result;
        }

        /// <summary>
        /// 标准无记录行
        /// </summary>
        public static string StandardNoRecordTr
        {
            get
            {
                return "<tr><td colspan=\"100\" class=\"text-center\">暂无记录.</td></tr>";
            }
        }

        /// <summary>
        /// 无数据符号
        /// </summary>
        public const string NASymbol = "N/A";


        private static string[] _brands = "reddit,adn,android,apple,bitbucket,bitbucket-square,bitcoin,btc,css3,dribbble,dropbox,facebook,facebook-square,flickr,foursquare,github,github-alt,github-square,gittip,google-plus,google-plus-square,html5,instagram,linkedin,linkedin-square,linux,maxcdn,pagelines,pinterest,pinterest-square,renren,skype,stack-exchange,stack-overflow,trello,tumblr,tumblr-square,twitter,twitter-square,vimeo-square,vk,weibo,windows,xing,xing-square,youtube,youtube-play,youtube-square".Split(',');
        /// <summary>
        /// 获取链接图标css
        /// </summary>
        public static string GetLinkIconCss(string link)
        {
            var result = "fa fa-link";
            if (link.StartsWith("mailto"))
                result = "fa fa-envelope";
            else
            {
                foreach (var b in _brands)
                {
                    if (link.ToLower().Contains(b))
                    {
                        result = $"fa fa-{b}";
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取价格显示文字
        /// </summary>
        /// <param name="price"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static string GetPriceDisplayText(decimal price, int decimals = 2)
        {
            price = price * 1.00M;
            var bigger = false;
            if (price > 1000)
            {
                bigger = true;
                price = price / 1000M;
            }
            price = decimal.Round(price, decimals);
            var result = bigger ? $"{price}k" : price.ToString();
            return result;
        }

        /// <summary>
        /// 获取价格列表显示文字
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string GetPriceTdDisplayText(decimal price)
        {
            return price > 0 ? price.TrimEndDecimalZero().ToString() : "-";
        }

        /// <summary>
        /// 获取数字显示文字
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetDisplayNumber(decimal number)
        {
            number = number.TrimEndDecimalZero();
            var result = number.ToString();
            var arr = result.Split('.');
            var s = arr[0];
            if (s.Length > 3)
            {
                var str = "";
                while (s.Length > 3)
                {
                    str += $"{s.Substring(0, 3)},";
                    s = s.Substring(3);
                }
                str += s;

                str += arr.Length > 1 ? $".{arr[1]}" : "";
                result = str;
            }

            return result;
        }

        /// <summary>
        /// 获取所有枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetAllEnumTypes<T>() where T : struct
        {
            var list = new List<T>();
            var type = typeof(T);
            foreach (var v in type.GetEnumValues())
            {
                list.Add((T)v);
            }

            return list;
        }

        public static bool HasAnyInterfaceAuthority(this List<InterfaceOperationModel> auths, string interfaceName)
        {
            return auths.Any(x => x.InterfaceName == interfaceName);
        }

        public static bool HasAnyInterfaceAuthority(this List<InterfaceOperationModel> auths, params string[] interfaceNames)
        {
            return auths.Any(x => interfaceNames.Contains(x.InterfaceName));
        }

        public static bool HasOperaAuthority(this List<InterfaceOperationModel> auths, string interfaceName,string operationName)
        {
            return auths.Any(x => x.InterfaceName == interfaceName && x.OperationName == operationName);
        }
    }
}
