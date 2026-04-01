using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class ValidByteAttribute : ValidationAttribute
    {
        private readonly byte _min;
        private readonly byte _max;

        public ValidByteAttribute(byte min = 0, byte max = 255)
        {
            _min = min;
            _max = max;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is byte b && (b < _min || b > _max))
                return new ValidationResult(
                    $"{validationContext.DisplayName} doit être entre {_min} et {_max}."
                );

            return ValidationResult.Success;
        }
    }
}