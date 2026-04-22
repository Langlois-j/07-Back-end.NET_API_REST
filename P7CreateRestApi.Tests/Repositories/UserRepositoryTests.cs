using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace P7CreateRestApi.Tests.Repositories
{
    public class UserRepositoryTests
    {
        // Crée un UserManager réel en s'appuyant sur un IdentityDbContext InMemory
        private static (UserRepository repo, UserManager<User> manager) CreateRepository()
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new LocalDbContext(options);

            var store = new UserStore<User>(context);

            var userManager = new UserManager<User>(
                store,
                Options.Create(new IdentityOptions()),
                new PasswordHasher<User>(),
                new List<IUserValidator<User>> { new UserValidator<User>() },
                new List<IPasswordValidator<User>> { new PasswordValidator<User>() },
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null!,
                new Logger<UserManager<User>>(new LoggerFactory())
            );

            return (new UserRepository(userManager), userManager);
        }


        [Fact]
        public async Task Add_ShouldCreateUser_WhenValidCredentials()
        {
            // Arrange
            var (repo, _) = CreateRepository();
            var user = new User { UserName = "jdupont", Fullname = "Jean Dupont"};

            // Act
            var result = await repo.Add(user, "Password123!");

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task Add_ShouldFail_WhenPasswordTooWeak()
        {
            // Arrange
            var (repo, _) = CreateRepository();
            var user = new User { UserName = "testuser" };

            // Act
            var result = await repo.Add(user, "123");

            // Assert
            Assert.False(result.Succeeded);
        }

       

        [Fact]
        public async Task FindAll_ShouldReturnAllUsers()
        {
            // Arrange
            var (repo, _) = CreateRepository();
            await repo.Add(new User { UserName = "user1"}, "Password123!");
            await repo.Add(new User { UserName = "user2" }, "Password123!");

            // Act
            var result = await repo.FindAll();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindAll_ShouldReturnEmptyList_WhenNoUsers()
        {
            // Arrange
            var (repo, _) = CreateRepository();

            // Act
            var result = await repo.FindAll();

            // Assert
            Assert.Empty(result);
        }

      

        [Fact]
        public async Task FindById_ShouldReturnCorrectUser()
        {
            // Arrange
            var (repo, manager) = CreateRepository();
            var user = new User { UserName = "jmartin" };
            await repo.Add(user, "Password123!");
            var created = await manager.FindByNameAsync("jmartin");

            // Act
            var result = await repo.FindById(created!.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("jmartin", result.UserName);
        }

        [Fact]
        public async Task FindById_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var (repo, _) = CreateRepository();

            // Act
            var result = await repo.FindById("id-inexistant");

            // Assert
            Assert.Null(result);
        }

        

        [Fact]
        public async Task FindByUserName_ShouldReturnCorrectUser()
        {
            // Arrange
            var (repo, _) = CreateRepository();
            await repo.Add(new User { UserName = "mleblanc"}, "Password123!");

            // Act
            var result = await repo.FindByUserName("mleblanc");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("mleblanc", result.UserName);
        }

        [Fact]
        public async Task FindByUserName_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var (repo, _) = CreateRepository();

            // Act
            var result = await repo.FindByUserName("utilisateur-inconnu");

            // Assert
            Assert.Null(result);
        }

       

        [Fact]
        public async Task Update_ShouldModifyUser()
        {
            // Arrange
            var (repo, manager) = CreateRepository();
            await repo.Add(new User { UserName = "avant"}, "Password123!");
            var user = await manager.FindByNameAsync("avant");
            user!.Fullname = "Nom Modifié";

            // Act
            var result = await repo.Update(user);

            // Assert
            Assert.True(result.Succeeded);
            var updated = await repo.FindById(user.Id);
            Assert.Equal("Nom Modifié", updated!.Fullname);
        }

 

        [Fact]
        public async Task Delete_ShouldRemoveUser()
        {
            // Arrange
            var (repo, manager) = CreateRepository();
            await repo.Add(new User { UserName = "asupprimer" }, "Password123!");
            var user = await manager.FindByNameAsync("asupprimer");

            // Act
            var result = await repo.Delete(user!);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(await repo.FindById(user!.Id));
        }
    }
}
