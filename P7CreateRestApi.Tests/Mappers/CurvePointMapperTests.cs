using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;

namespace P7CreateRestApi.Tests.Mappers
{
    public class CurvePointMapperTests
    {
        private readonly CurveMapper _mapper = new CurveMapper();

        [Fact]
        public void ToDTO_ShouldMapAllFields()
        {
            // Arrange
            var entity = new CurvePoint
            {
                Id = 1,
                CurveId = (byte)5,
                Term = 12.5,
                CurvePointValue = 99.9
            };

            // Act
            var dto = _mapper.ToDTO(entity);

            // Assert
            Assert.Equal(1, dto.Id);
            Assert.Equal((byte)5, dto.CurveId);
            Assert.Equal(12.5, dto.Term);
            Assert.Equal(99.9, dto.CurvePointValue);
        }

        [Fact]
        public void ToEntity_ShouldMapAllFields()
        {
            // Arrange
            var dto = new CurveDTO
            {
                Id = 2,
                CurveId = (byte)10,
                Term = 6.0,
                CurvePointValue = 50.0
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(2, entity.Id);
            Assert.Equal((byte)10, entity.CurveId);
            Assert.Equal(6.0, entity.Term);
            Assert.Equal(50.0, entity.CurvePointValue);
        }
    }
}