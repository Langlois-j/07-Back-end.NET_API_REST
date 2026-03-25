using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;

namespace Dot.Net.WebApi.Mappers
{
    public class UserMapper : IMapper<User, UserDTO>
    {
        public UserDTO ToDTO(User entity)
        {
            return new UserDTO
            {
                Id = entity.Id,
                Username = entity.Username,
                Fullname = entity.Fullname,
                Role = entity.Role
            };
        }

        public User ToEntity(UserDTO dto)
        {
            return new User
            {
                Id = dto.Id,
                Username = dto.Username ?? string.Empty,
                Fullname = dto.Fullname ?? string.Empty,
                Role = dto.Role ?? string.Empty
            };
        }
    }
}