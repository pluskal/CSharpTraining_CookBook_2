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