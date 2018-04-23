using DCache.Cache;
using DCache.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DCache.Containers
{
    /// <summary>
    /// IBaseContainerBag，BaseContainer容器中的Value类型
    /// </summary>
    public interface IBaseContainerBag
    {
        /// <summary>
        /// 用于标记，方便后台管理
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 缓存键
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// 当前对象被缓存的时间
        /// </summary>
        DateTime CacheTime { get; set; }
    }

    
    /// <summary>
    /// BaseContainer容器中的Value类型
    /// </summary>
    [Serializable]
    public class BaseContainerBag : BindableBase, IBaseContainerBag
    {
        private string _key;
        private string _name;

        /// <summary>
        /// 用于标记，方便后台管理
        /// </summary>
        public string Name
        {
            get { return _name; }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _name, value, "Name"); }
#else
            set { this.SetContainerProperty(ref _name, value); }
#endif
        }

        /// <summary>
        /// 键
        /// </summary>
        public string Key
        {
            get { return _key; }
#if NET35 || NET40
            set { this.SetContainerProperty(ref _key, value, "Key"); }
#else
            set { this.SetContainerProperty(ref _key, value); }
#endif
        }

        /// <summary>
        /// 缓存时间，不使用属性变化监听
        /// </summary>
        public DateTime CacheTime { get; set; }


        public BaseContainerBag()
        {
            base.PropertyChanged += BaseContainerBag_PropertyChanged;
        }


        /// <summary>
        /// 设置Container属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
#if NET35 || NET40
        protected bool SetContainerProperty<T>(ref T storage, T value, String propertyName)
#else
        protected bool SetContainerProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
#endif
        {
            var result = base.SetProperty(ref storage, value, propertyName);
            return result;
        }

        private void BaseContainerBag_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var containerBag = (IBaseContainerBag)sender;
            //var containerCacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance().ContainerCacheStrategy;
            //var itemCacheKey = ContainerHelper.GetItemCacheKey(containerBag);
            //containerBag.CacheTime = DateTime.Now;//记录缓存时间
            //containerCacheStrategy.UpdateContainerBag(itemCacheKey, containerBag);
        }

    }
}
