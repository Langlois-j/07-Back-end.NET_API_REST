using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class Int_ValidAttribute : ValidationAttribute
    {
        public const string ErrorCodeOutOfRange = "INT_OUT_OF_RANGE";
        private readonly int _min;
        private readonly int _max;
        private readonly string? _fieldName;

        public Int_ValidAttribute(string? fieldName = null,int min = int.MinValue, int max = int.MaxValue)
        {
            _min = min;
            _max = max;
            _fieldName = fieldName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is int i && (i < _min || i > _max))
            {
                var name = _fieldName ?? validationContext.DisplayName;
                return new ValidationResult(
                    $"{validationContext.DisplayName} doit être entre {_min} et {_max}.",
                    new[] { ErrorCodeOutOfRange });
            }

            return ValidationResult.Success;
        }
    }
}