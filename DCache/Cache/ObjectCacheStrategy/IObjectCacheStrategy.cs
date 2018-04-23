using DCache.Cache.CacheStrategy;
using DCache.Cache.ContainerCacheStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCache.Cache.ObjectCacheStrategy
{
    /// <summary>
    /// 所有以String类型为Key的缓存策略接口
    /// </summary>
    public interface IObjectCacheStrategy : IBaseCacheStrategy<string, object>
    {
        IContainerCacheStrategy ContainerCacheStrategy { get; }
    }

}
