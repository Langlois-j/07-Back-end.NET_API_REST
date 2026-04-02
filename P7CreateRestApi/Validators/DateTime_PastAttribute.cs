using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dot.Net.WebApi.Validators
{
    public class DateTime_PastAttribute : ValidationAttribute
    {
        public const string ErrorCodeFutureDate = "DATE_IN_FUTURE";
        private readonly bool _allowToday;
        private readonly string? _fieldName;

        public DateTime_PastAttribute(string? fieldName = null, bool allowToday = true)
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

                if (_allowToday && compareDate > today)
                    return new ValidationResult(
                        $"{name} ne peut pas être dans le futur.",
                        new[] { ErrorCodeFutureDate });

                if (!_allowToday && compareDate >= today)
                    return new ValidationResult(
                        $"{name} ne peut pas être aujourd'hui ou dans le futur.",
                        new[] { ErrorCodeFutureDate });
            }

            return ValidationResult.Success;
        }
    }
}