using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class RatingRepository : BaseRepository<Rating>
    {
        public RatingRepository(LocalDbContext dbContext) : base(dbContext) { }

        protected override DbSet<Rating> DbSet => _dbContext.Ratings;
    }
}