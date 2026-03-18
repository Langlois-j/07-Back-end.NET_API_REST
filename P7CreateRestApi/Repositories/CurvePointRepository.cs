using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class CurvePointRepository : IRepository<CurvePoint>
    {
        private readonly LocalDbContext _dbContext;

        public CurvePointRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Lier à l'interface
        public async Task<List<CurvePoint>> FindAll()
        {
            return await _dbContext.CurvePoints.ToListAsync();
        }

        public async Task<CurvePoint?> FindById(int id)
        {
            return await _dbContext.CurvePoints.FindAsync(id);
        }
        public async Task<CurvePoint> Add(CurvePoint entity)
        {
            _dbContext.CurvePoints.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<CurvePoint?> Update(int id, CurvePoint entity)
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
            _dbContext.CurvePoints.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}