using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class PositiveDoubleAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is double d && d < 0)
                return new ValidationResult($"{validationContext.DisplayName} doit être un nombre positif.");

            return ValidationResult.Success;
        }
    }
}