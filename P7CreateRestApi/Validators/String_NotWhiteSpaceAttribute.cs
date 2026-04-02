using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace Dot.Net.WebApi.Validators
{
    public class String_NotWhiteSpaceAttribute : ValidationAttribute
    {
        public const string ErrorCodeWhiteSpace = "STRING_WHITESPACE_ONLY";
        public const string ErrorCodeEmpty = "STRING_EMPTY";
        private readonly string? _fieldName;

        public String_NotWhiteSpaceAttribute(string? fieldName = null)
        {
            _fieldName = fieldName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var str = value as string;
            var name = _fieldName ?? validationContext.DisplayName;

            if (string.IsNullOrEmpty(str))
                return new ValidationResult(
                    $"{name} ne peut pas être vide.",
                    new[] { ErrorCodeEmpty });

            if (string.IsNullOrWhiteSpace(str))
                return new ValidationResult(
                    $"{name} ne peut pas contenir uniquement des espaces.",
                    new[] { ErrorCodeWhiteSpace });

            return ValidationResult.Success;
        }
    }
}