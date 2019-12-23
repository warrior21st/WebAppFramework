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
        private static ConcurrentDictionary<string, Dictionary<string, string>> _cacheDescs = new ConcurrentDictionary<string, Dictionary<string, string>>();

        public static string GetDescription<T>(T model) where T : Enum
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
        public static Dictionary<string, string> GetEnumDescriptions<T>() where T : Enum
        {
            var dic = new Dictionary<string, string>();
            var t = typeof(T);
            var key = $"enum_description_cache_{t.FullName}";
            if (!_cacheDescs.ContainsKey(key))
            {
                var fields = t.GetFields();
                foreach (var field in fields)
                {
                    var descAttr = field.GetCustomAttribute<DescriptionAttribute>();
                    if (descAttr != null)
                        dic.Add(field.Name, descAttr.Description);
                }

                _cacheDescs.TryAdd(key, dic);
            }

            _cacheDescs.TryGetValue(key, out var dic2);
            return dic2;
        }

        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        private static List<int> GetAllTypes<T>() where T : Enum
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
        /// 获取包含的成员
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<int> GetSumContainsMembers(int n)
        {
            var list = new List<int>();
            var e = 1;
            var temp = Convert.ToUInt32(n);
            while (temp > 1)
            {
                temp >>= 1;
                e += 1;
            }

            var val = Convert.ToUInt32(n);
            for (var i = 0; i < e; i++)
            {
                if (val << 31 - i >> 31 == 1)
                    list.Add(Convert.ToInt32(0b_1 << i));
            }

            return list;
        }

        public static List<int> GetContainsMemberSums<T>(T member) where T : Enum
        {
            var max = GetAllTypes<T>().Max() << 1;
            var list = new List<uint>();
            var bits = 0;
            var temp = Convert.ToUInt32(member);
            while (temp > 1)
            {
                temp >>= 1;
                bits += 1;
            }

            var val = Convert.ToUInt32(member);
            for (var i = val; i < max; i++)
            {
                if (i << 31 - bits >> 31 == 1)
                {
                    list.Add(i);
                }
            }

            return list.Select(x => Convert.ToInt32(x)).ToList();
        }

        /// <summary>
        /// 是否包含所有
        /// </summary>
        /// <param name="nts"></param>
        /// <returns></returns>
        public static bool IsContainsAll<T>(int nts) where T : Enum
        {
            var list = GetSumContainsMembers(nts).ToList();
            var all = GetAllTypes<T>();
            return list.Sum(x => x) == all.Sum(x => x);
        }
    }
}
