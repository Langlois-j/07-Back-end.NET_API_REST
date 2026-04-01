using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class ValidDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is DateTime date && date > DateTime.UtcNow)
                return new ValidationResult($"{validationContext.DisplayName} ne peut pas être dans le futur.");

            return ValidationResult.Success;
        }
    }
}