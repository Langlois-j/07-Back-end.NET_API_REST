using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class Double_PositiveAttribute : ValidationAttribute
    {
        public const string ErrorCodeNegative = "DOUBLE_NEGATIVE";
        private readonly string? _fieldName;
        public Double_PositiveAttribute(string? fieldName = null) 
        {
            _fieldName = fieldName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is double d && d < 0)
            {
                var name = _fieldName ?? validationContext.DisplayName;
                return new ValidationResult(
                    $"{name} doit être un nombre positif.",
                    new[] { ErrorCodeNegative });
            }
            return ValidationResult.Success;
        }
    }
}