using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace P7CreateRestApi.Tests.Helpers
{
    public static class MockTokenHelper
    {
        public static string GenerateToken(string role = "User")
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "Jwt:SecretKey", "une-cle-secrete-suffisamment-longue" },
                    { "Jwt:Issuer",    "TestIssuer" },
                    { "Jwt:Audience",  "TestAudience" }
                })
                .Build();

            var mockUserManager = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null!, null!, null!, null!, null!, null!, null!, null!
            );
            mockUserManager
                .Setup(m => m.GetRolesAsync(It.IsAny<User>()))
              .ReturnsAsync(role == "Admin"
        ? new List<string> { "Admin", "User" }
        : new List<string> { role });

            var service = new TokenService(mockUserManager.Object, config);

            var user = new User { Id = "test-id", UserName = "testuser" };

            return service.GenerateToken(user).Result;
        }
    }
}