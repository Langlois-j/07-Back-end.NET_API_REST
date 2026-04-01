using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.Domain
{
    public class User : BaseEntity
    {
 
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        public string? Username { get; set; }
        [Required]
        [PasswordValidation]
        public string? Password { get; set; }
       // [Required(ErrorMessage = "Le nom complet est obligatoire.")]
        public string? Fullname { get; set; }
        [Required(ErrorMessage = "Le rôle est obligatoire.")]
        public string? Role { get; set; }
    }
}