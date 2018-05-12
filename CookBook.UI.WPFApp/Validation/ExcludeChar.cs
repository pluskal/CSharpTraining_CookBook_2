using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CookBook.UI.WPFApp.Validation
{
    public class ExcludeChar : ValidationAttribute
    {
        private readonly String _characters;

        public ExcludeChar(String characters)
        {
            this._characters = characters;
        }

        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            var valueAsString = value.ToString();

            foreach (var character in this._characters)
            {
                if (!valueAsString.Contains(character)) continue;

                var errorMessage = this.FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}