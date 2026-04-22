using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = Guid.NewGuid().ToString(); // ← unique par instance

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Jwt:SecretKey", "une-cle-secrete-suffisamment-longue");
        builder.UseSetting("Jwt:Issuer", "TestIssuer");
        builder.UseSetting("Jwt:Audience", "TestAudience");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<LocalDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<LocalDbContext>(options =>
                options.UseInMemoryDatabase(_dbName)); // ← nom unique

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var roleManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.RoleExistsAsync("User").Result)
                roleManager.CreateAsync(new IdentityRole("User")).Wait();
            if (!roleManager.RoleExistsAsync("Admin").Result)
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
        });
    }
}