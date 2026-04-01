using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;


namespace Dot.Net.WebApi.Mappers
{
    public class TradeMapper : IMapper<Trade, TradeDTO>
    {
        public TradeDTO ToDTO(Trade entity)
        {
            return new TradeDTO
            {
       Id =entity.Id,
       Account = entity.Account,
       AccountType = entity.AccountType,
       BuyQuantity = entity.BuyQuantity,
       SellQuantity = entity.SellQuantity,
       BuyPrice = entity.BuyPrice,
       SellPrice = entity.SellPrice,
       TradeDate = entity.TradeDate,
       TradeSecurity = entity.TradeSecurity,
       TradeStatus = entity.TradeStatus,
       Trader = entity.Trader,
       Benchmark = entity.Benchmark,
       Book = entity.Book,
       RevisionName = entity.RevisionName,
       RevisionDate = entity.RevisionDate,
       DealName = entity.DealName,
       DealType = entity.DealType,
       SourceListId = entity.SourceListId,
       Side = entity.Side,
            };
        }

        public Trade ToEntity(TradeDTO dto)
        {
            return new Trade
            {
                Id = dto.Id,
                Account = dto.Account ?? string.Empty,
                AccountType = dto.AccountType ?? string.Empty,
                BuyQuantity = dto.BuyQuantity,
                SellQuantity = dto.SellQuantity,
                BuyPrice = dto.BuyPrice,
                SellPrice = dto.SellPrice,
                TradeDate = dto.TradeDate,
                TradeSecurity = dto.TradeSecurity ?? string.Empty,
                TradeStatus = dto.TradeStatus ?? string.Empty,
                Trader = dto.Trader ?? string.Empty,
                Benchmark = dto.Benchmark ?? string.Empty,
                Book = dto.Book ?? string.Empty,
                RevisionName = dto.RevisionName ?? string.Empty,
                RevisionDate = dto.RevisionDate,
                DealName = dto.DealName ?? string.Empty,
                DealType = dto.DealType ?? string.Empty,
                SourceListId = dto.SourceListId ?? string.Empty,
                Side = dto.Side ?? string.Empty,
            };
        }
    }
}