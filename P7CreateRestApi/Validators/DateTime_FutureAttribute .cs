using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dot.Net.WebApi.Validators
{
    public class DateTime_FutureAttribute : ValidationAttribute
    {
        public const string ErrorCodePastDate = "DATE_IN_PAST";
        private readonly bool _allowToday;
        private readonly string? _fieldName;

        public DateTime_FutureAttribute(string? fieldName = null, bool allowToday = true)
        {
            _allowToday = allowToday;
            _fieldName = fieldName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is DateTime date)
            {
                var today = DateTime.UtcNow.Date;
                var compareDate = date.Date;
                var name = _fieldName ?? validationContext.DisplayName;

                if (_allowToday && compareDate < today)
                    return new ValidationResult(
                        $"{name} ne peut pas être dans le passé.",
                        new[] { ErrorCodePastDate });
                

                if (!_allowToday && compareDate <= today)
                    return new ValidationResult(
                        $"{name} ne peut pas être aujourd'hui ou dans le passé.",
                        new[] { ErrorCodePastDate });
               
            }

            return ValidationResult.Success;
        }
    }
}