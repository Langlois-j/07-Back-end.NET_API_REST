using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;

namespace P7CreateRestApi.Tests.Mappers
{
    public class UserMapperTests
    {
        private readonly UserMapper _mapper = new UserMapper();

        [Fact]
        public void ToDTO_ShouldMapAllFields()
        {
            // Arrange
            var entity = new User
            {
                Id = "user-guid-123",
                UserName = "jdupont",
                Fullname = "Jean Dupont",
                Role = "Admin"
            };

            // Act
            var dto = _mapper.ToDTO(entity);

            // Assert
            Assert.Equal("user-guid-123", dto.Id);
            Assert.Equal("jdupont", dto.UserName);
            Assert.Equal("Jean Dupont", dto.Fullname);
            Assert.Equal("Admin", dto.Role);
        }

        [Fact]
        public void ToEntity_ShouldMapAllFields()
        {
            // Arrange
            var dto = new UserDTO
            {
                Id = "user-guid-456",
                UserName = "mmartin",
                Fullname = "Marie Martin",
                Role = "User"
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal("user-guid-456", entity.Id);
            Assert.Equal("mmartin", entity.UserName);
            Assert.Equal("Marie Martin", entity.Fullname);
            Assert.Equal("User", entity.Role);
        }

        [Fact]
        public void ToEntity_ShouldReplaceNullStringsWithEmpty()
        {
            // Arrange
            var dto = new UserDTO
            {
                UserName = null,
                Fullname = null,
                Role = null
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(string.Empty, entity.UserName);
            Assert.Equal(string.Empty, entity.Fullname);
            Assert.Equal(string.Empty, entity.Role);
        }
    }
}
