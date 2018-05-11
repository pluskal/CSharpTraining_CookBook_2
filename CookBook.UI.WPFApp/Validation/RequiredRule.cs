using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Castle.Core.Internal;

namespace CookBook.UI.WPFApp.Validation
{
    public class RequiredRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value is string item && item.IsNullOrEmpty())
            {
                return new ValidationResult(false, "Field is required.");
            }
            return ValidationResult.ValidResult;
        }
    }
}