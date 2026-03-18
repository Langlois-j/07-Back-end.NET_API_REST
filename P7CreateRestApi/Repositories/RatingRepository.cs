using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class RatingRepository : IRepository<Rating>
    {
        private readonly LocalDbContext _dbContext;

        public RatingRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Lier à l'interface
        public async Task<List<Rating>> FindAll()
        {
            return await _dbContext.Ratings.ToListAsync();
        }

        public async Task<Rating?> FindById(int id)
        {
            return await _dbContext.Ratings.FindAsync(id);
        }
        public async Task<Rating> Add(Rating entity)
        {
            _dbContext.Ratings.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Rating?> Update(int id, Rating entity)
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

            _dbContext.Ratings.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}