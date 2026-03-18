using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class RuleNameRepository : IRepository<RuleName>
    {
        private readonly LocalDbContext _dbContext;

        public RuleNameRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Lier à l'interface
        public async Task<List<RuleName>> FindAll()
        {
            return await _dbContext.RuleNames.ToListAsync();
        }

        public async Task<RuleName?> FindById(int id)
        {
            return await _dbContext.RuleNames.FindAsync(id);
        }
        public async Task<RuleName> Add(RuleName entity)
        {
            _dbContext.RuleNames.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<RuleName?> Update(int id, RuleName entity)
        {
            var existing = await FindById(id);
            if (existing == null) return null;

            _dbContext.Entry(existing).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await FindById(id);
            if (existing == null) return false;

            _dbContext.RuleNames.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}