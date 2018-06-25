using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCache.Memcached.ObjectCacheStrategy;
using DCache.Cache.ContainerCacheStrategy;
using DCache.Containers;
#if NET45 || NET461

#else
//using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Logging;
#endif


namespace DCache.Memcached.ContainerCacheStrategy
{
    public class MemcachedContainerStrategy : MemcachedObjectCacheStrategy, IContainerCacheStrategy
    {

        #region 单例
        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        MemcachedContainerStrategy()
        {
        }

        //静态LocalCacheStrategy
        public static new IContainerCacheStrategy Instance
        {
            get
            {
                return Nested.instance;//返回Nested类中的静态成员instance
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            //将instance设为一个初始化的LocalCacheStrategy新实例
            internal static readonly MemcachedContainerStrategy instance = new MemcachedContainerStrategy();
        }
        #endregion


        #region IContainerCacheStrategy 成员

        public void InsertToCache(string key, IBaseContainerBag value)//TODO:添加Timeout参数
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return;
            }

            base.InsertToCache(key, value);
#if DEBUG
            var cacheKey = GetFinalKey(key);
            value = _cache.Get(cacheKey) as IBaseContainerBag;
#endif
        }

        public override void RemoveFromCache(string key, bool isFullKey = false)
        {
            base.RemoveFromCache(key, isFullKey);
        }

        public new IBaseContainerBag Get(string key, bool isFullKey = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            var cacheKey = GetFinalKey(key, isFullKey);
            return _cache.Get<IBaseContainerBag>(cacheKey);
        }

        public IDictionary<string, TBag> GetAll<TBag>() where TBag : IBaseContainerBag
        {
            throw new NotImplementedException();
        }

        public new IDictionary<string, IBaseContainerBag> GetAll()
        {
            throw new NotImplementedException();
        }

        public override bool CheckExisted(string key, bool isFullKey = false)
        {
            return base.CheckExisted(key, isFullKey);
        }

        public override long GetCount()
        {
            throw new NotImplementedException();//TODO:需要定义二级缓存键，从池中获取
        }

        public new void Update(string key, IBaseContainerBag value, bool isFullKey = false)
        {
            base.Update(key, value, isFullKey);
        }

        public void UpdateContainerBag(string key, IBaseContainerBag containerBag, bool isFullKey = false)
        {
            var cacheKey = GetFinalKey(key, isFullKey);
            object value;
            if (_cache.TryGet(cacheKey, out value))
            {
                Update(cacheKey, containerBag, true);
            }
        }

        #endregion

    }
}
