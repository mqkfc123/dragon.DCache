using DCache.Cache;
using DCache.Cache.ObjectCacheStrategy;
using DCache.Redis.StackExchange.Redis;
using DCache.RegisterServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCache.Redis
{

    public static class Register
    {
        /// <summary>
        /// 注册 Redis 缓存信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="redisConfigurationString">Redis的连接字符串</param>
        /// <param name="redisObjectCacheStrategyInstance">缓存策略的委托，第一个参数为 redisConfigurationString</param>
        /// <returns></returns>
        public static IRegisterService RegisterCacheRedis(this IRegisterService registerService,
            string redisConfigurationString,
            Func<string, IObjectCacheStrategy> redisObjectCacheStrategyInstance)
        {
            RedisManager.ConfigurationOption = redisConfigurationString;

            //此处先执行一次委托，直接在下方注册结果，提高每次调用的执行效率
            IObjectCacheStrategy objectCacheStrategy = redisObjectCacheStrategyInstance(redisConfigurationString);
            if (objectCacheStrategy != null)
            {
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => objectCacheStrategy);//Redis
            }

            return registerService;
        }


    }
}
