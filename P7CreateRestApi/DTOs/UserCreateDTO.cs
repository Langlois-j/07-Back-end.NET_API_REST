using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.DTOs
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [String_PasswordValidation]
        public string Password { get; set; } = string.Empty;

        public string? Fullname { get; set; }
        [Required(ErrorMessage = "Le rôle est obligatoire.")]
        [AllowedValues(UserRoles.User, UserRoles.Admin,
            ErrorMessage = "Le rôle doit être 'User' ou 'Admin'.")]
        public string Role { get; set; } = string.Empty;
    }
}