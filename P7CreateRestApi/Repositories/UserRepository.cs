using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(LocalDbContext dbContext) : base(dbContext) { }

        protected override DbSet<User> DbSet => _dbContext.Users;
    }
}