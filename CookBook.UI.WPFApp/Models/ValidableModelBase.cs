using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CookBook.UI.WPFApp.Models
{
    public class ValidableModelBase : BindableBase, IDataErrorInfo
    {
        private readonly HashSet<string> _erroneousProperties = new HashSet<string>();

        public bool HasErrors => _erroneousProperties.Any();

        public string this[string propertyName] => this.OnValidate(propertyName);

        private string OnValidate(string propertyName)
        {
            if(string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property does not exist");

            var error = string.Empty;
            var value = this.GetValue(propertyName);
            var results = new List<ValidationResult>();

            var result = Validator.TryValidateProperty(
                value,
                new ValidationContext(this, null, null)
                {
                    MemberName = propertyName
                },
                results);

            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
                this._erroneousProperties.Add(propertyName);
            }
            else
            {
                this._erroneousProperties.Remove(propertyName);
            }

            return error;
        }

        public string Error => throw new NotSupportedException();
    }
}