using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class MaxValueAttribute : ValidationAttribute
    {
        private readonly double _max;

        public MaxValueAttribute(double max)
        {
            _max = max;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is double d && d > _max)
                return new ValidationResult(
                    $"{validationContext.DisplayName} doit être inférieur à {_max}."
                );

            return ValidationResult.Success;
        }
    }
}