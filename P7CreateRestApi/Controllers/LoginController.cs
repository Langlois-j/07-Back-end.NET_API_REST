using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Models;
using Dot.Net.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(UserManager<User> userManager, ITokenService tokenService,ILogger<LoginController> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                _logger.LogWarning("Tentative de connexion échouée : utilisateur '{UserName}' introuvable.", model.UserName);
                return Unauthorized();
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                _logger.LogWarning("Tentative de connexion échouée : mot de passe incorrect pour '{UserName}'.", model.UserName);
                return Unauthorized();
            }

            var token = await _tokenService.GenerateToken(user);
            _logger.LogInformation("Connexion réussie pour l'utilisateur '{UserName}'.", model.UserName);
            return Ok(new { token });
        }


    }
}