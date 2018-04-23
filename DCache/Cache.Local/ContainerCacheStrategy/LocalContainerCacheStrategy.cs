using DCache.Cache.ContainerCacheStrategy;
using DCache.Cache.Local.ObjectCacheStrategy;
using DCache.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCache.Cache.Local.ContainerCacheStrategy
{

    /// <summary>
    /// 本地容器缓存策略
    /// </summary>
    public sealed class LocalContainerCacheStrategy : LocalObjectCacheStrategy, IContainerCacheStrategy
    //where TContainerBag : class, IBaseContainerBag, new()
    {
        #region 数据源

        private IDictionary<string, object> _cache = LocalObjectCacheHelper.LocalObjectCache;

        #endregion

        #region 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        LocalContainerCacheStrategy() : base()
        {
        }

        //静态LocalCacheStrategy
        public static IContainerCacheStrategy Instance
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
            internal static readonly LocalContainerCacheStrategy instance = new LocalContainerCacheStrategy();
        }

        #endregion

        #region ILocalCacheStrategy 成员

        public void InsertToCache(string key, IBaseContainerBag value)
        {
            base.InsertToCache(key, value);
        }

        public void RemoveFromCache(string key, bool isFullKey = false)
        {
            base.RemoveFromCache(key, isFullKey);
        }

        public IBaseContainerBag Get(string key, bool isFullKey = false)
        {
            var cache = base.Get(key, isFullKey) as IBaseContainerBag;
            if (cache.CacheTime <= DateTime.Now)
            {
                base.RemoveFromCache(key, isFullKey);
                return base.Get(key, isFullKey) as IBaseContainerBag;
            }
            else
            {
                return cache;
            }
        }


        public IDictionary<string, TBag> GetAll<TBag>() where TBag : IBaseContainerBag
        {
            var dic = new Dictionary<string, TBag>();
            var cacheList = GetAll();
            foreach (var baseContainerBag in cacheList)
            {
                if (baseContainerBag.Value is TBag)
                {
                    dic[baseContainerBag.Key] = (TBag)baseContainerBag.Value;
                }
            }
            return dic;
        }

        public IDictionary<string, IBaseContainerBag> GetAll()
        {
            var dic = new Dictionary<string, IBaseContainerBag>();
            foreach (var item in _cache)
            {
                if (item.Value is IBaseContainerBag)
                {
                    dic[item.Key] = (IBaseContainerBag)item.Value;
                }
            }
            return dic;
        }

        public bool CheckExisted(string key, bool isFullKey = false)
        {
            var cacheKey = GetFinalKey(key, isFullKey);
            return _cache.ContainsKey(cacheKey);
        }

        public long GetCount()
        {
            return GetAll().Count;
        }

        public void Update(string key, IBaseContainerBag value, bool isFullKey = false)
        {
            base.Update(key, value, isFullKey);
        }

        public void UpdateContainerBag(string key, IBaseContainerBag bag, bool isFullKey = false)
        {
            Update(key, bag, isFullKey);
        }

        #endregion
    }
}
