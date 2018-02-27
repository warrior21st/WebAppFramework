using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonHelpers.Helpers
{
    /// <summary>
    /// 内存缓存操作类
    /// </summary>
    public static class MemoryCacheHelper
    {
        private readonly static IMemoryCache _cache;

        static MemoryCacheHelper(/*MemoryCacheOptions options*/)
        {
            //this._cache = new MemoryCache(options);
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        /// <summary>
        /// 检查缓存是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            object v = null;
            return _cache.TryGetValue<object>(key, out v);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public static T GetCache<T>(string key) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            T v = null;
            _cache.TryGetValue<T>(key, out v);

            return v;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        public static void SetCache(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            object v = null;
            if (_cache.TryGetValue(key, out v))
                _cache.Remove(key);
            SetCache(key,value,TimeSpan.FromHours(2));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="slidingExpirationTime">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expirationTime">绝对过期时间</param>
        public static void SetCache(string key, object value, TimeSpan slidingExpirationTime, DateTimeOffset? expirationTime = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            object v = null;
            if (_cache.TryGetValue(key, out v))
                _cache.Remove(key);
            var option = new MemoryCacheEntryOptions().SetSlidingExpiration(slidingExpirationTime);
            if (expirationTime != null)
                option.SetAbsoluteExpiration(Convert.ToDateTime(expirationTime));
            _cache.Set<object>(key, value, option);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expirationTime">绝对过期时间</param>
        public static void SetCache(string key, object value, DateTimeOffset expirationTime)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            object v = null;
            if (_cache.TryGetValue(key, out v))
                _cache.Remove(key);

            _cache.Set<object>(key, value, expirationTime);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public static void RemoveCache(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (Exists(key))
                _cache.Remove(key);
        }

        //public void Dispose()
        //{
        //    if (_cache != null)
        //        _cache.Dispose();
        //    GC.SuppressFinalize(this);
        //}
    }
}
