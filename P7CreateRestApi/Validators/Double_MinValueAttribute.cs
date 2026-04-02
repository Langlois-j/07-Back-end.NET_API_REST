using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class Double_MinValueAttribute : ValidationAttribute
    {
        public const string ErrorCodeBelowMin = "DOUBLE_BELOW_MIN";
        private readonly double _min;
        private readonly string? _fieldName;

        public Double_MinValueAttribute(string? fieldName = null, double min=0)
        {
            _min = min;
            _fieldName = fieldName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is double d && d <= _min)
            {
                var name = _fieldName ?? validationContext.DisplayName;
                return new ValidationResult(
                    $"{name} doit être supérieur à {_min}.",
                    new[] { ErrorCodeBelowMin });
            }
            return ValidationResult.Success;
        }
    }
}