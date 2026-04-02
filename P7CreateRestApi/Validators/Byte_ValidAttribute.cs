using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class Byte_ValidAttribute : ValidationAttribute
    {
        public const string ErrorCodeOutOfRange = "BYTE_OUT_OF_RANGE";
        private readonly byte _min;
        private readonly byte _max;
        private readonly string? _fieldName;

        public Byte_ValidAttribute(string? fieldName = null,byte min = 0, byte max = 255)
        {
            _min = min;
            _max = max;
            _fieldName = fieldName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is byte b && (b < _min || b > _max))
            {
                var name = _fieldName ?? validationContext.DisplayName;
                return new ValidationResult(
                $"{name} doit être entre {_min} et {_max}.",
                new[] { ErrorCodeOutOfRange });
        }
            return ValidationResult.Success;
        }
    }
}