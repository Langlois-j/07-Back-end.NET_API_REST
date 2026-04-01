using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class CurvePointRepository : BaseRepository<CurvePoint>
    {
        public CurvePointRepository(LocalDbContext dbContext) : base(dbContext) { }

        protected override DbSet<CurvePoint> DbSet => _dbContext.CurvePoints;
    }
}

