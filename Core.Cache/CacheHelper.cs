using System;
using System.Web;
using System.Web.Caching;

namespace Core.Cache
{
    public class CacheHelper
    {
        /// <summary>
        /// 本地缓存获取
        /// </summary>
        /// <param name="name">key</param>
        /// <returns></returns>
        public static object Get(string name)
        {
            return HttpRuntime.Cache.Get(name);
        }

        /// <summary>
        /// 本地缓存移除
        /// </summary>
        /// <param name="name">key</param>
        public static void Remove(string name)
        {
            if (HttpRuntime.Cache[name] != null)
                HttpRuntime.Cache.Remove(name);
        }

        /// <summary>
        /// 本地缓存写入（默认缓存20min）
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="value">value</param>
        public static void Set(string name, object value)
        {
            Set(name, value, null);
        }
        /// <summary>
        /// 本地缓存写入（默认缓存20min）,依赖项
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="value">value</param>
        /// <param name="cacheDependency">依赖项</param>
        public static void Set(string name, object value, CacheDependency cacheDependency)
        {
            HttpRuntime.Cache.Insert(name, value, cacheDependency, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
        }

        /// <summary>
        /// 本地缓存写入
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="value">value</param>
        /// <param name="minutes">缓存分钟</param>
        public static void Set(string name, object value, int minutes)
        {
            HttpRuntime.Cache.Insert(name, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes));
        }
    }
}
