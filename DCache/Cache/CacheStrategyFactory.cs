using DCache.Cache.ContainerCacheStrategy;
using DCache.Cache.Local.ObjectCacheStrategy;
using DCache.Cache.ObjectCacheStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCache.Cache
{
    public class CacheStrategyFactory
    {
        internal static Func<IObjectCacheStrategy> ObjectCacheStrateFunc;
        /// <summary>
        /// 注册当前全局环境下的缓存策略
        /// </summary>
        /// <param name="func">如果为null，将使用默认的本地缓存策略（LocalObjectCacheStrategy.Instance）</param>
        public static void RegisterObjectCacheStrategy(Func<IObjectCacheStrategy> func)
        {
            ObjectCacheStrateFunc = func;
        }

        /// <summary>
        /// 如果
        /// </summary>
        /// <returns></returns>
        public static IObjectCacheStrategy GetObjectCacheStrategyInstance()
        {
            if (ObjectCacheStrateFunc == null)
            {
                //默认状态
                return LocalObjectCacheStrategy.Instance;
            }
            else
            {
                //自定义类型
                var instance = ObjectCacheStrateFunc();// ?? LocalObjectCacheStrategy.Instance;
                return instance;
            }
        }

    }
}
