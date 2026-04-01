using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Validators
{
    public class ValidIntegerAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public ValidIntegerAttribute(int min = int.MinValue, int max = int.MaxValue)
        {
            _min = min;
            _max = max;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is int i && (i < _min || i > _max))
                return new ValidationResult(
                    $"{validationContext.DisplayName} doit être entre {_min} et {_max}."
                );

            return ValidationResult.Success;
        }
    }
}