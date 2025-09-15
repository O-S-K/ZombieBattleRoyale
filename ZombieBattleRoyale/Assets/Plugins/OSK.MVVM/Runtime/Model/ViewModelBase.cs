using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OSK.MVVM
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, Action<object>> _bindings = new Dictionary<string, Action<object>>();

        protected void Notify([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (_bindings.TryGetValue(propertyName, out var cb))
                cb?.Invoke(GetType().GetProperty(propertyName)?.GetValue(this));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            Notify(propertyName);
            return true;
        }

        public void RegisterBinding(string propName, Action<object> callback)
        {
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == propName)
                    callback(GetType().GetProperty(propName)?.GetValue(this));
            };

            var prop = GetType().GetProperty(propName);
            if (prop != null)
                callback(prop.GetValue(this));
        }

        protected void RaisePropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}