using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class RuleNameRepository : BaseRepository<RuleName>
    {
        public RuleNameRepository(LocalDbContext dbContext) : base(dbContext) { }

        protected override DbSet<RuleName> DbSet => _dbContext.RuleNames;
    }
}