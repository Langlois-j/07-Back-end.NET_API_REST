using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        public string? UserName { get; set; }
        public string? Fullname { get; set; }
        [AllowedValues(UserRoles.User, UserRoles.Admin, ErrorMessage = "Le rôle doit être 'User' ou 'Admin'.")]
        public string? Role { get; set; }
    }
}