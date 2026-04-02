using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class Double_MaxValueAttribute : ValidationAttribute
    {
        public const string ErrorCodeAboveMax = "DOUBLE_ABOVE_MAX";
        private readonly double _max;
        private readonly string? _fieldName;

        public Double_MaxValueAttribute(string? fieldName = null, double max=99)
        {
            _max = max;
            _fieldName = fieldName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is double d && d > _max)
            {
                var name = _fieldName ?? validationContext.DisplayName;
                return new ValidationResult(
                    $"{validationContext.DisplayName} doit être inférieur à {_max}.",
                    new[] { ErrorCodeAboveMax });
            }

            return ValidationResult.Success;
        }
    }
}