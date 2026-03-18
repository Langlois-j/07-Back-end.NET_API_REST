using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class TradeRepository : IRepository<Trade>
    {
        private readonly LocalDbContext _dbContext;

        public TradeRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Lier à l'interface
        public async Task<List<Trade>> FindAll()
        {
            return await _dbContext.Trades.ToListAsync();
        }

        public async Task<Trade?> FindById(int id)
        {
            return await _dbContext.Trades.FindAsync(id);
        }
        public async Task<Trade> Add(Trade entity)
        {
            _dbContext.Trades.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Trade?> Update(int id, Trade entity)
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

            _dbContext.Trades.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}