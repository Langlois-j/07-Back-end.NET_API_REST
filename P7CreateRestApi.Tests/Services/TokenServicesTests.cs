using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace P7CreateRestApi.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly IConfiguration _config;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly TokenService _service;

        public TokenServiceTests()
        {
            // Configuration JWT en mémoire
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "Jwt:SecretKey", "une-cle-secrete-suffisamment-longue" },
                    { "Jwt:Issuer",    "TestIssuer" },
                    { "Jwt:Audience",  "TestAudience" }
                })
                .Build();

            // Mock du UserManager
            _mockUserManager = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null!, null!, null!, null!, null!, null!, null!, null!
            );

            _service = new TokenService(_mockUserManager.Object, _config);
        }

        [Fact]
        public async Task GenerateToken_ShouldReturnNonEmptyString()
        {
            // Arrange
            var user = new User { Id = "abc-123", UserName = "jdupont" };
            _mockUserManager
                .Setup(m => m.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            // Act
            var token = await _service.GenerateToken(user);

            // Assert
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public async Task GenerateToken_ShouldContainUserName()
        {
            // Arrange
            var user = new User { Id = "abc-123", UserName = "jdupont" };
            _mockUserManager
                .Setup(m => m.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            // Act
            var token = await _service.GenerateToken(user);

            // Décode le token pour inspecter les claims
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert
            var subClaim = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            Assert.NotNull(subClaim);
            Assert.Equal("jdupont", subClaim.Value);
        }

        [Fact]
        public async Task GenerateToken_ShouldContainRole()
        {
            // Arrange
            var user = new User { Id = "abc-123", UserName = "jdupont" };
            _mockUserManager
                .Setup(m => m.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "Admin" });

            // Act
            var token = await _service.GenerateToken(user);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert
            var roleClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            Assert.NotNull(roleClaim);
            Assert.Equal("Admin", roleClaim.Value);
        }

        [Fact]
        public async Task GenerateToken_ShouldExpireInOneHour()
        {
            // Arrange
            var user = new User { Id = "abc-123", UserName = "jdupont" };
            _mockUserManager
                .Setup(m => m.GetRolesAsync(user))
                .ReturnsAsync(new List<string>());

            // Act
            var token = await _service.GenerateToken(user);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert — expiration dans ~1h (on tolère 5 secondes d'écart)
            var expectedExpiry = DateTime.UtcNow.AddHours(1);
            Assert.True(Math.Abs((jwt.ValidTo - expectedExpiry).TotalSeconds) < 5);
        }
    }
}