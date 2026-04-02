using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;

namespace P7CreateRestApi.Tests.Mappers
{
    public class BidListMapperTests
    {
        private readonly BidListMapper _mapper = new BidListMapper();

        [Fact]
        public void ToDTO_ShouldMapAllFields()
        {
            // Arrange
            var entity = new BidList
            {
                Id = 1,
                Account = "TestAccount",
                BidType = "TypeA",
                BidQuantity = 10.5
            };

            // Act
            var dto = _mapper.ToDTO(entity);

            // Assert
            Assert.Equal(1, dto.Id);
            Assert.Equal("TestAccount", dto.Account);
            Assert.Equal("TypeA", dto.BidType);
            Assert.Equal(10.5, dto.BidQuantity);
        }

        [Fact]
        public void ToEntity_ShouldMapAllFields()
        {
            // Arrange
            var dto = new BidListDTO
            {
                Id = 2,
                Account = "DtoAccount",
                BidType = "TypeB",
                BidQuantity = 20.0
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(2, entity.Id);
            Assert.Equal("DtoAccount", entity.Account);
            Assert.Equal("TypeB", entity.BidType);
            Assert.Equal(20.0, entity.BidQuantity);
        }

        [Fact]
        public void ToEntity_ShouldReplaceNullStringsWithEmpty()
        {
            // Arrange
            var dto = new BidListDTO { Account = null, BidType = null };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(string.Empty, entity.Account);
            Assert.Equal(string.Empty, entity.BidType);
        }
    }
}