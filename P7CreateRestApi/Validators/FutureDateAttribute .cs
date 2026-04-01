using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date && date < DateTime.UtcNow)
            return new ValidationResult("La date ne peut pas être dans le passé.");

        return ValidationResult.Success;
    }
}