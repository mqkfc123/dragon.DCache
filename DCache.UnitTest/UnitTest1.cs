using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DCache.RegisterServices; 
using System.Configuration;
using DCache.Redis.ObjectCacheStrategy;
using DCache.Redis;
using DCache.Cache;
using DCache.Containers;

namespace DCache.UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        public UnitTest1()
        {
            //注册开始
            //RegisterService.Start()
            //    //配置Redis缓存
            //    .RegisterCacheRedis(
            //        ConfigurationManager.AppSettings["Cache_Redis_Configuration"],
            //        redisConfiguration => (!string.IsNullOrEmpty(redisConfiguration) && redisConfiguration != "Redis配置")
            //                             ? RedisObjectCacheStrategy.Instance
            //                             : null);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var containerCacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance().ContainerCacheStrategy;
            var data = new StudentCache();
            data.CacheTime = DateTime.Now.AddMinutes(15);
            data.data = "缓存测试";
            containerCacheStrategy.InsertToCache("dragon1", data);
            var obj = containerCacheStrategy.Get("dragon1");
        }

        [Serializable]
        public class StudentCache : BaseContainerBag
        {
            public object data { get; set; }

        }


    }
}
