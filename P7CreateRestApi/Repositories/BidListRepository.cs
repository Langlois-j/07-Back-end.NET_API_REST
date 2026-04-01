using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class BidListRepository : BaseRepository<BidList>
    {
        public BidListRepository(LocalDbContext dbContext) : base(dbContext) { }

        protected override DbSet<BidList> DbSet => _dbContext.BidLists;
    }
}