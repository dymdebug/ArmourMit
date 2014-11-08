using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using RMTFRK.Core.Data;
using RMTFRK.Web.Helper;

namespace RMTFRK.Web.Data.Cache
{
    public class NCacheHelper
    {
        public static void InsertFile(string key, object obj, string fileName)
        {
            CacheDependency dep = new CacheDependency(fileName);
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }

        public static void Insert(string key, object obj)
        {
            if (obj != null)
            {
                int expires = CommonHelper.GetInt(ConfigHelper.GetAppSettings("TimeCache"));
                HttpContext.Current.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
            }
        }

        public static bool IsExist(string strKey)
        {
            return HttpContext.Current.Cache[strKey] != null;
        }

        public static object GetCache(string key)
        {
            object result;
            if (string.IsNullOrEmpty(key))
            {
                result = null;
            }
            else
            {
                if (ConfigHelper.GetAppSettings("IsCache") == "false")
                {
                    result = null;
                }
                else
                {
                    result = HttpContext.Current.Cache.Get(key);
                }
            }
            return result;
        }

        public static T Get<T>(string key)
        {
            object obj = NCacheHelper.GetCache(key);
            return (obj == null) ? default(T) : ((T)((object)obj));
        }

        public static void RemoveAllCache(string CacheKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(CacheKey);
        }

        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}