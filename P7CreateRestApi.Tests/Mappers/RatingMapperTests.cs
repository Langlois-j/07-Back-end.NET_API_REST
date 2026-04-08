using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;

namespace P7CreateRestApi.Tests.Mappers
{
    public class RatingMapperTests
    {
        private readonly RatingMapper _mapper = new RatingMapper();

        [Fact]
        public void ToDTO_ShouldMapAllFields()
        {
            // Arrange
            var entity = new Rating
            {
                Id = 1,
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA",
                OrderNumber = (byte)3
            };

            // Act
            var dto = _mapper.ToDTO(entity);

            // Assert
            Assert.Equal(1, dto.Id);
            Assert.Equal("Aaa", dto.MoodysRating);
            Assert.Equal("AAA", dto.SandPRating);
            Assert.Equal("AAA", dto.FitchRating);
            Assert.Equal((byte)3, dto.OrderNumber);
        }

        [Fact]
        public void ToEntity_ShouldMapAllFields()
        {
            // Arrange
            var dto = new RatingDTO
            {
                Id = 2,
                MoodysRating = "Baa",
                SandPRating = "BBB",
                FitchRating = "BBB",
                OrderNumber = (byte)5
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(2, entity.Id);
            Assert.Equal("Baa", entity.MoodysRating);
            Assert.Equal("BBB", entity.SandPRating);
            Assert.Equal("BBB", entity.FitchRating);
            Assert.Equal((byte)5, entity.OrderNumber);
        }

        [Fact]
        public void ToEntity_ShouldReplaceNullStringsWithEmpty()
        {
            // Arrange
            var dto = new RatingDTO
            {
                MoodysRating = null,
                SandPRating = null,
                FitchRating = null
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(string.Empty, entity.MoodysRating);
            Assert.Equal(string.Empty, entity.SandPRating);
            Assert.Equal(string.Empty, entity.FitchRating);
        }
    }
}