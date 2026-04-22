using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}