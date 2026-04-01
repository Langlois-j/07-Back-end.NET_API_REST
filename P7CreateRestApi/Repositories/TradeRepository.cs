using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class TradeRepository : BaseRepository<Trade>
    {
        public TradeRepository(LocalDbContext dbContext) : base(dbContext) { }

        protected override DbSet<Trade> DbSet => _dbContext.Trades;
    }
}