using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Tests.Helpers
{
    public static class TestDbContextFactory
    {
        public static LocalDbContext Create()
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new LocalDbContext(options);
        }
    }
}