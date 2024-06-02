using System.ComponentModel.DataAnnotations;

namespace FakeIt.Web.Filters
{
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedValues;

        public AllowedValuesAttribute(params string[] allowedValues)
        {
            _allowedValues = allowedValues;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // You can change this if null is not allowed.
            }

            if (_allowedValues.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"The value '{value}' is not valid. Allowed values are: {string.Join(", ", _allowedValues)}.");
        }
    }
}
