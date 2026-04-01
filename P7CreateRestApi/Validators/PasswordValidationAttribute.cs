using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dot.Net.WebApi.Validators
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password))
                return new ValidationResult("Le mot de passe est obligatoire.");

            if (password.Length < 8)
                return new ValidationResult("Le mot de passe doit contenir au moins 8 caractères.");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult("Le mot de passe doit contenir au moins une majuscule.");

            if (!Regex.IsMatch(password, @"[0-9]"))
                return new ValidationResult("Le mot de passe doit contenir au moins un chiffre.");

            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                return new ValidationResult("Le mot de passe doit contenir au moins un symbole.");

            return ValidationResult.Success;
        }
    }
}