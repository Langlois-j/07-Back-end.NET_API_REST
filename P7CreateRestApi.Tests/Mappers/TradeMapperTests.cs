using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;

namespace P7CreateRestApi.Tests.Mappers
{
    public class TradeMapperTests
    {
        private readonly TradeMapper _mapper = new TradeMapper();

        [Fact]
        public void ToDTO_ShouldMapAllFields()
        {
            // Arrange
            var tradeDate = new DateTime(2025, 6, 1);
            var revisionDate = new DateTime(2025, 12, 1);

            var entity = new Trade
            {
                Id = 1,
                Account = "AccountA",
                AccountType = "TypeA",
                BuyQuantity = 100.0,
                SellQuantity = 50.0,
                BuyPrice = 10.5,
                SellPrice = 11.0,
                TradeDate = tradeDate,
                TradeSecurity = "Security1",
                TradeStatus = "Open",
                Trader = "TraderA",
                Benchmark = "BenchA",
                Book = "BookA",
                RevisionName = "RevA",
                RevisionDate = revisionDate,
                DealName = "DealA",
                DealType = "DealTypeA",
                SourceListId = "SRC001",
                Side = "Buy"
            };

            // Act
            var dto = _mapper.ToDTO(entity);

            // Assert
            Assert.Equal(1, dto.Id);
            Assert.Equal("AccountA", dto.Account);
            Assert.Equal("TypeA", dto.AccountType);
            Assert.Equal(100.0, dto.BuyQuantity);
            Assert.Equal(50.0, dto.SellQuantity);
            Assert.Equal(10.5, dto.BuyPrice);
            Assert.Equal(11.0, dto.SellPrice);
            Assert.Equal(tradeDate, dto.TradeDate);
            Assert.Equal("Security1", dto.TradeSecurity);
            Assert.Equal("Open", dto.TradeStatus);
            Assert.Equal("TraderA", dto.Trader);
            Assert.Equal("BenchA", dto.Benchmark);
            Assert.Equal("BookA", dto.Book);
            Assert.Equal("RevA", dto.RevisionName);
            Assert.Equal(revisionDate, dto.RevisionDate);
            Assert.Equal("DealA", dto.DealName);
            Assert.Equal("DealTypeA", dto.DealType);
            Assert.Equal("SRC001", dto.SourceListId);
            Assert.Equal("Buy", dto.Side);
        }

        [Fact]
        public void ToEntity_ShouldMapAllFields()
        {
            // Arrange
            var tradeDate = new DateTime(2025, 6, 1);
            var revisionDate = new DateTime(2025, 12, 1);

            var dto = new TradeDTO
            {
                Id = 2,
                Account = "AccountB",
                AccountType = "TypeB",
                BuyQuantity = 200.0,
                SellQuantity = 75.0,
                BuyPrice = 20.0,
                SellPrice = 21.5,
                TradeDate = tradeDate,
                TradeSecurity = "Security2",
                TradeStatus = "Closed",
                Trader = "TraderB",
                Benchmark = "BenchB",
                Book = "BookB",
                RevisionName = "RevB",
                RevisionDate = revisionDate,
                DealName = "DealB",
                DealType = "DealTypeB",
                SourceListId = "SRC002",
                Side = "Sell"
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(2, entity.Id);
            Assert.Equal("AccountB", entity.Account);
            Assert.Equal("TypeB", entity.AccountType);
            Assert.Equal(200.0, entity.BuyQuantity);
            Assert.Equal(75.0, entity.SellQuantity);
            Assert.Equal(20.0, entity.BuyPrice);
            Assert.Equal(21.5, entity.SellPrice);
            Assert.Equal(tradeDate, entity.TradeDate);
            Assert.Equal("Security2", entity.TradeSecurity);
            Assert.Equal("Closed", entity.TradeStatus);
            Assert.Equal("TraderB", entity.Trader);
            Assert.Equal("BenchB", entity.Benchmark);
            Assert.Equal("BookB", entity.Book);
            Assert.Equal("RevB", entity.RevisionName);
            Assert.Equal(revisionDate, entity.RevisionDate);
            Assert.Equal("DealB", entity.DealName);
            Assert.Equal("DealTypeB", entity.DealType);
            Assert.Equal("SRC002", entity.SourceListId);
            Assert.Equal("Sell", entity.Side);
        }

        [Fact]
        public void ToEntity_ShouldReplaceNullStringsWithEmpty()
        {
            // Arrange
            var dto = new TradeDTO
            {
                Account = null,
                AccountType = null,
                TradeSecurity = null,
                TradeStatus = null,
                Trader = null,
                Benchmark = null,
                Book = null,
                RevisionName = null,
                DealName = null,
                DealType = null,
                SourceListId = null,
                Side = null
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(string.Empty, entity.Account);
            Assert.Equal(string.Empty, entity.AccountType);
            Assert.Equal(string.Empty, entity.TradeSecurity);
            Assert.Equal(string.Empty, entity.TradeStatus);
            Assert.Equal(string.Empty, entity.Trader);
            Assert.Equal(string.Empty, entity.Benchmark);
            Assert.Equal(string.Empty, entity.Book);
            Assert.Equal(string.Empty, entity.RevisionName);
            Assert.Equal(string.Empty, entity.DealName);
            Assert.Equal(string.Empty, entity.DealType);
            Assert.Equal(string.Empty, entity.SourceListId);
            Assert.Equal(string.Empty, entity.Side);
        }
    }
}