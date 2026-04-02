using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        public string Password { get; set; } = string.Empty;
    }
}