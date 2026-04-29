using Microsoft.AspNetCore.Identity;
using Dot.Net.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class User : IdentityUser
    {
        [Required]
        public override string UserName { get; set; } = string.Empty;

        [String_NotWhiteSpace("Compte")]
        public string? Fullname { get; set; }
    }
}