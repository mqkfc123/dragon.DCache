using DCache.Properties.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DCache.Entities
{
    /// <summary>
    /// 用于实现INotifyPropertyChanged
    /// </summary>
    [Serializable]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
#if NET35 || NET40
        protected virtual void OnPropertyChanged(string propertyName)
#else
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
#endif
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }

            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//需要VS2015+或新编译器支持
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// #if NET35 || NET40
#if NET35 || NET40
        protected bool SetProperty<T>(ref T storage, T value, String propertyName)
#else
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
#endif

        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
    }
}
