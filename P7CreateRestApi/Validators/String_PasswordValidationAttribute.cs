using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dot.Net.WebApi.Validators
{
    public class String_PasswordValidationAttribute : ValidationAttribute
    {
        public const string ErrorCodeRequired = "PASSWORD_REQUIRED";
        public const string ErrorCodeMinLength = "PASSWORD_MIN_LENGTH";
        public const string ErrorCodeUpperCase = "PASSWORD_UPPERCASE_REQUIRED";
        public const string ErrorCodeDigit = "PASSWORD_DIGIT_REQUIRED";
        public const string ErrorCodeSymbol = "PASSWORD_SYMBOL_REQUIRED";




        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;
         

            if (string.IsNullOrEmpty(password))
                return new ValidationResult(
                    "Le mot de passe est obligatoire.",
                    new[] { ErrorCodeRequired });

            if (password.Length < 8)
                return new ValidationResult(
                    "Le mot de passe doit contenir au moins 8 caractères.",
                    new[] { ErrorCodeMinLength });

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult(
                    "Le mot de passe doit contenir au moins une majuscule.",
                    new[] { ErrorCodeUpperCase });

            if (!Regex.IsMatch(password, @"[0-9]"))
                return new ValidationResult(
                    "Le mot de passe doit contenir au moins un chiffre.",
                    new[] { ErrorCodeDigit });

            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                return new ValidationResult(
                    "Le mot de passe doit contenir au moins un symbole.",
                    new[] { ErrorCodeSymbol });

            return ValidationResult.Success;
        }
    }
}