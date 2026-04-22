using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace P7CreateRestApi.Tests.Helpers
{
    public static class IdentityTestFactory
    {
        public static (UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
            Create(LocalDbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(
                userStore,
                Options.Create(new IdentityOptions()),
                new PasswordHasher<User>(),
                new List<IUserValidator<User>> { new UserValidator<User>() },
                new List<IPasswordValidator<User>> { new PasswordValidator<User>() },
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
               null!,
                new Logger<UserManager<User>>(new LoggerFactory())
            );

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(
                roleStore,
                new List<IRoleValidator<IdentityRole>> { new RoleValidator<IdentityRole>() },
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                new Logger<RoleManager<IdentityRole>>(new LoggerFactory())
            );

            return (userManager, roleManager);
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
    }
}