using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly LocalDbContext _dbContext;
        protected abstract DbSet<T> DbSet { get; }

        protected BaseRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> FindAll() =>
            await DbSet.ToListAsync();

        public async Task<T?> FindById(int id) =>
            await DbSet.FindAsync(id);

        public async Task<T> Add(T entity)
        {
            DbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> Update(int id, T entity)
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

            DbSet.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}