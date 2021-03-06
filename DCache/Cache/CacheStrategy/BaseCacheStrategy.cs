﻿using DCache.Cache.Lock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCache.Cache.CacheStrategy
{
    /// <summary>
    /// 泛型缓存策略基类
    /// </summary>
    public abstract class BaseCacheStrategy : IBaseCacheStrategy
    {
        /// <summary>
        /// 获取拼装后的FinalKey
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isFullKey">是否已经是经过拼接的FullKey</param>
        /// <returns></returns>
        public string GetFinalKey(string key, bool isFullKey = false)
        {
            return isFullKey ? key : String.Format("DCache:{0}:{1}", "DefaultCache", key);
        }

        /// <summary>
        /// 获取一个同步锁
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="key"></param>
        /// <param name="retryCount"></param>
        /// <param name="retryDelay"></param>
        /// <returns></returns>
        public abstract ICacheLock BeginCacheLock(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = new TimeSpan());
    }
}
