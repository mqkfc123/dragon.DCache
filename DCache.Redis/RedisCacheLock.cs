using DCache.Cache.Lock;
using DCache.Redis.ObjectCacheStrategy;
using DCache.Redlock.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace DCache.Redis
{
    /// <summary>
    /// redis分布式锁
    /// </summary>
    public class RedisCacheLock : BaseCacheLock
    {
        private Redlock.CSharp.Redlock _dlm;
        private Lock _lockObject;

        private RedisObjectCacheStrategy _redisStrategy;

        public RedisCacheLock(RedisObjectCacheStrategy strategy, string resourceName, string key, int retryCount, TimeSpan retryDelay)
            : base(strategy, resourceName, key, retryCount, retryDelay)
        {
            _redisStrategy = strategy;
            LockNow();//立即等待并抢夺锁
        }

        public override bool Lock(string resourceName)
        {
            return Lock(resourceName, 0, new TimeSpan());
        }

        public override bool Lock(string resourceName, int retryCount, TimeSpan retryDelay)
        {
            if (retryCount != 0)
            {
                _dlm = new Redlock.CSharp.Redlock(retryCount, retryDelay, _redisStrategy._client);
            }
            else if (_dlm == null)
            {
                _dlm = new Redlock.CSharp.Redlock(_redisStrategy._client);
            }

            var ttl = (retryDelay.TotalMilliseconds > 0 ? retryDelay.TotalMilliseconds : 10)
                       *
                      (retryCount > 0 ? retryCount : 10);


            var successfull = _dlm.Lock(resourceName, TimeSpan.FromMilliseconds(ttl), out _lockObject);
            return successfull;
        }

        public override void UnLock(string resourceName)
        {
            if (_lockObject != null)
            {
                _dlm.Unlock(_lockObject);
            }
        }
    }
}
