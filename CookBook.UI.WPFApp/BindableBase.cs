using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Castle.Core.Internal;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using CookBook.UI.WPFApp.Annotations;

namespace CookBook.UI.WPFApp
{
    public class BindableBase : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _propertyBackingFields
            = new Dictionary<string, object>();

        protected void SetValue<T>(Expression<Func<T>> propertySelector, T value)
        {
            var propertyName = GetPropertyName(propertySelector);
            SetValue<T>(propertyName, value);
        }

        private void SetValue<T>(string propertyName, T value)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Empty property name");

            this._propertyBackingFields[propertyName] = value;
            this.OnPropertyChanged(propertyName);
        }

        protected object GetValue(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Empty property name");
            if (this._propertyBackingFields.TryGetValue(propertyName, out var value)) return value;

            var propertyDescriptor = TypeDescriptor.GetProperties(this.GetType())
                                                   .Find(propertyName,false);
            if(propertyDescriptor == null)
                throw new ArgumentException("Invalid property name");

            value = propertyDescriptor.GetValue(this);
            this._propertyBackingFields.Add(propertyName,value);

            return value;
        }

        protected T GetValue<T>(Expression<Func<T>> propertySelector)
        {
            var propertyName = GetPropertyName(propertySelector);
            return GetValue<T>(propertyName);
        }

        protected T GetValue<T>(string propertyName)
        {
            if(string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Empty property name");

            if (this._propertyBackingFields.TryGetValue(propertyName, out var backingFieldValue))
                return (T) backingFieldValue;

            backingFieldValue = default(T);
            this._propertyBackingFields.Add(propertyName, backingFieldValue);

            return (T) backingFieldValue;
        }

        private string GetPropertyName(LambdaExpression expression)
        {
            if (!(expression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException();

            return memberExpression.Member.Name;
        }

        #region INotifyPropertyChanged inmplementation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}