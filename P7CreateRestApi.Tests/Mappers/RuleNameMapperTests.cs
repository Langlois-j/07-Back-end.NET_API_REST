using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;

namespace P7CreateRestApi.Tests.Mappers
{
    public class RuleNameMapperTests
    {
        private readonly RuleNameMapper _mapper = new RuleNameMapper();

        [Fact]
        public void ToDTO_ShouldMapAllFields()
        {
            // Arrange
            var entity = new RuleName
            {
                Id = 1,
                Name = "Rule1",
                Description = "Une description",
                Json = "{ }",
                Template = "template1",
                SqlStr = "SELECT *",
                SqlPart = "WHERE id = 1"
            };

            // Act
            var dto = _mapper.ToDTO(entity);

            // Assert
            Assert.Equal(1, dto.Id);
            Assert.Equal("Rule1", dto.Name);
            Assert.Equal("Une description", dto.Description);
            Assert.Equal("{ }", dto.Json);
            Assert.Equal("template1", dto.Template);
            Assert.Equal("SELECT *", dto.SqlStr);
            Assert.Equal("WHERE id = 1", dto.SqlPart);
        }

        [Fact]
        public void ToEntity_ShouldMapAllFields()
        {
            // Arrange
            var dto = new RuleNameDTO
            {
                Id = 2,
                Name = "Rule2",
                Description = "Autre description",
                Json = "{ \"key\": \"value\" }",
                Template = "template2",
                SqlStr = "SELECT id",
                SqlPart = "FROM table"
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(2, entity.Id);
            Assert.Equal("Rule2", entity.Name);
            Assert.Equal("Autre description", entity.Description);
            Assert.Equal("{ \"key\": \"value\" }", entity.Json);
            Assert.Equal("template2", entity.Template);
            Assert.Equal("SELECT id", entity.SqlStr);
            Assert.Equal("FROM table", entity.SqlPart);
        }

        [Fact]
        public void ToEntity_ShouldReplaceNullStringsWithEmpty()
        {
            // Arrange
            var dto = new RuleNameDTO
            {
                Name = null,
                Description = null,
                Json = null,
                Template = null,
                SqlStr = null,
                SqlPart = null
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(string.Empty, entity.Name);
            Assert.Equal(string.Empty, entity.Description);
            Assert.Equal(string.Empty, entity.Json);
            Assert.Equal(string.Empty, entity.Template);
            Assert.Equal(string.Empty, entity.SqlStr);
            Assert.Equal(string.Empty, entity.SqlPart);
        }
    }
}