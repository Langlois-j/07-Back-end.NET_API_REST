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
                UserName = entity.UserName ?? string.Empty,
                Fullname = entity.Fullname,
            };
        }

        public User ToEntity(UserDTO dto)
        {
            return new User
            {
                Id = dto.Id,
                UserName = dto.UserName ?? string.Empty,
                Fullname = dto.Fullname ?? string.Empty,
                
            };
        }
    }
}