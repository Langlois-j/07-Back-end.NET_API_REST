using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly double _min;

        public MinValueAttribute(double min)
        {
            _min = min;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is double d && d <= _min)
                return new ValidationResult(
                    $"{validationContext.DisplayName} doit être supérieur à {_min}."
                );

            return ValidationResult.Success;
        }
    }
}