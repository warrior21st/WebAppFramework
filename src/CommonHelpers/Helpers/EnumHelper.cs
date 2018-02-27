using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Reflection;
using System.Linq;

namespace CommonHelpers.Helpers
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetEnumDescription<T>(T model) where T : struct
        {
            var valueStr = model.ToString();
            var result = valueStr;
            var dic = GetEnumDescriptions<T>();
            if (dic.ContainsKey(valueStr))
                result = dic[valueStr];

            return result;
        }

        /// <summary>
        /// 获取枚举描述集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static Dictionary<string, string> GetEnumDescriptions<T>() where T : struct
        {
            var dic = new Dictionary<string, string>();
            var t = typeof(T);
            var key = $"enum_description_cache_{t.FullName}";
            if (!MemoryCacheHelper.Exists(key))
            {
                var fields = t.GetFields();
                foreach (var field in fields)
                {
                    var descAttr = field.GetCustomAttribute<DescriptionAttribute>();
                    if (descAttr != null)
                        dic.Add(field.Name, descAttr.Description);
                }

                MemoryCacheHelper.SetCache(key, dic);
            }

            return MemoryCacheHelper.GetCache<Dictionary<string, string>>(key);
        }

        /// <summary>
        /// 获取多选域名类型包含的数值
        /// </summary>
        /// <param name="nts"></param>
        /// <returns></returns>
        public static List<int> GetTypesContains(int nts)
        {
            var arr = GetTypesByNumber(nts);
            var list = GetMemberSums(arr.ToArray());
            return list;
        }



        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        private static List<int> GetAllTypes<T>() where T : struct
        {
            var allTypes = new List<int>();
            var tvs = Enum.GetValues(typeof(T));
            foreach (var tv in tvs)
            {
                allTypes.Add((int)tv);
            }

            return allTypes;
        }

        /// <summary>
        /// 获取所有成员的可能所有和
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        private static List<int> GetMemberSums(int[] enums)
        {
            var list = new List<int>();
            var arr = enums;
            var max = arr.Max().ToString();
            var maxIndex = 0;
            for (var i = max.Length - 1; i > -1; i--)
            {
                maxIndex += Convert.ToInt32(Math.Pow(2, i));
            }

            for (var i = 1; i <= maxIndex; i++)
            {
                var s = Convert.ToString(i, 2);
                var b = true;
                var l = s.Length;
                for (int j = 0; j < l; j++)
                {
                    if (s[j] == '1' && !arr.Contains(Convert.ToInt32(Math.Pow(10, l - j - 1))))
                    {
                        b = false;
                        break;
                    }
                }
                if (b && int.TryParse(s, out var n))
                {
                    list.Add(n);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取包含的成员
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static List<int> GetTypesByNumber(int n)
        {
            var list = new List<int>();
            var ns = n.ToString();
            var l = ns.Length;
            for (var i = 0; i < l; i++)
            {
                if (ns[i] == '1')
                    list.Add(Convert.ToInt32(Math.Pow(10, l - i - 1)));
            }

            return list;
        }

        /// <summary>
        /// 是否包含所有
        /// </summary>
        /// <param name="nts"></param>
        /// <returns></returns>
        public static bool IsContainsAll<T>(int nts) where T : struct
        {
            var list = GetTypesByNumber(nts).ToList();
            var all = GetAllTypes<T>();
            list.Sort();
            all.Sort();
            return string.Join(',', list) == string.Join(',', all);
        }

        #region 已弃用-递归求和算法

        ///// <summary>
        ///// 获取多选域名类型包含的数值
        ///// </summary>
        ///// <param name="nts"></param>
        ///// <returns></returns>
        //public static List<int> GetNameTypesContains(int nts)
        //{
        //    var enums = GetTypeListByNumber(nts);
        //    var results = new List<int>();
        //    //var allTypes = GetAllTypes();
        //    foreach (var nt in enums)
        //    {
        //        results.Add(nt);
        //        var arrTypes = new List<int>();
        //        foreach (var t in enums)
        //        {
        //            arrTypes.Add((t));
        //        }
        //        if (arrTypes.IndexOf(nt) != -1)
        //            arrTypes.Remove(nt);

        //        for (var i = 0; i < arrTypes.Count; i++)
        //        {
        //            var arrx = new List<int>();
        //            for (var j = i; j < arrTypes.Count; j++)
        //            {
        //                arrx.Add(arrTypes[j]);
        //            }
        //            AddNameTypes(nt, arrx, results);
        //        }
        //    }

        //    return results;
        //}

        ///// <summary>
        ///// 根据数值获取所有枚举类型
        ///// </summary>
        ///// <param name="nts"></param>
        ///// <returns></returns>
        //public static List<int> GetTypeListByNumber(int nts)
        //{
        //    var list = new List<int>();
        //    var s = nts.ToString();
        //    for (var i = 0; i < s.Length; i++)
        //    {
        //        if (int.TryParse(s[i].ToString(), out var nt) && nt == 1)
        //            list.Add(Convert.ToInt32(Math.Pow(10, s.Length - i - 1)));
        //    }

        //    return list;
        //}

        ///// <summary>
        ///// 添加名称类型到列表
        ///// </summary>
        ///// <param name="sum"></param>
        ///// <param name="arr"></param>
        ///// <param name="arrRes"></param>
        //private static void AddNameTypes(int sum, List<int> arr, List<int> arrRes)
        //{
        //    if (arr.Count == 1)
        //    {
        //        var n = sum + arr[0];
        //        if (!arrRes.Contains(n))
        //        {
        //            arrRes.Add(n);
        //        }

        //        return;
        //    }

        //    for (var i = 0; i < arr.Count; i++)
        //    {
        //        var n = sum + arr[i];
        //        if (!arrRes.Contains(n))
        //        {
        //            arrRes.Add(n);
        //        }
        //    }

        //    sum += arr[0];
        //    var arr2 = GetRemoveIndex0List(arr);

        //    AddNameTypes(sum, arr2, arrRes);
        //}


        ///// <summary>
        ///// 返回移除第一个元素的列表
        ///// </summary>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //private static List<int> GetRemoveIndex0List(List<int> list)
        //{
        //    var ls = new List<int>();
        //    if (list.Count > 1)
        //    {
        //        foreach (var i in list)
        //        {
        //            ls.Add(i);
        //        }
        //        ls.RemoveAt(0);
        //    }

        //    return ls;
        //}

        #endregion
    }
}
