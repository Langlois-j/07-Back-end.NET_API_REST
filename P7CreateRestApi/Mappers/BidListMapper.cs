using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using P7CreateRestApi.DTOs;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dot.Net.WebApi.Mappers
{
    public class BidListMapper : IMapper<BidList, BidListDTO>
    {
        public BidListDTO ToDTO(BidList entity)
        {
            return new BidListDTO
            {
                BidListId = entity.BidListId,
                Account = entity.Account,
                BidType = entity.BidType,
                BidQuantity = entity.BidQuantity,
                AskQuantity = entity.AskQuantity,
                Bid = entity.Bid,
                Ask = entity.Ask,
                Benchmark = entity.Benchmark,
                BidListDate = entity.BidListDate,
                Commentary = entity.Commentary,
                BidSecurity = entity.BidSecurity,
                BidStatus = entity.BidStatus,
                Trader = entity.Trader,
                Book = entity.Book,
                DealName = entity.DealName,
                DealType = entity.DealType,
                Side = entity.Side,
            };
        }

        public BidList ToEntity(BidListDTO dto)
        {
            return new BidList
            {
                BidListId = dto.BidListId,
                Account = dto.Account ?? string.Empty,
                BidType = dto.BidType ?? string.Empty,
                BidQuantity = dto.BidQuantity ,
                AskQuantity = dto.AskQuantity,
                Bid = dto.Bid ,
                Ask = dto.Ask,
                Benchmark = dto.Benchmark ?? string.Empty,
                BidListDate = dto.BidListDate ,
                Commentary = dto.Commentary ?? string.Empty,
                BidSecurity = dto.BidSecurity ?? string.Empty,
                BidStatus = dto.BidStatus ?? string.Empty,
                Trader = dto.Trader ?? string.Empty,
                Book = dto.Book ?? string.Empty,
                DealName = dto.DealName ?? string.Empty,
                DealType = dto.DealType ?? string.Empty,
                Side = dto.Side ?? string.Empty,
            };
        }
    }
}