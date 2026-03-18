using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class BidListRepository : IRepository<BidList>
    {
        private readonly LocalDbContext _dbContext;

        public BidListRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Lier à l'interface
        public async Task<List<BidList>> FindAll()
        {
            return await _dbContext.BidLists.ToListAsync();
        }

        public async Task<BidList?> FindById(int id)
        {
            return await _dbContext.BidLists.FindAsync(id);
        }
        public async Task<BidList> Add(BidList entity)
        {
            _dbContext.BidLists.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<BidList?> Update(int id, BidList entity)
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

            _dbContext.BidLists.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}