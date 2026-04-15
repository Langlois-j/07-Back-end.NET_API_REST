using Microsoft.AspNetCore.Identity;
using Dot.Net.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class User : IdentityUser
    {
        [String_NotWhiteSpace("Compte")]
        public string? Fullname { get; set; }
    }
}