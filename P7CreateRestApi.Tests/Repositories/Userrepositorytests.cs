using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddUser()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            var repository = new UserRepository(context);
            var user = new User { Username = "TestUser", Password = "Test@1234", Role = "Admin" };

            // Act
            var result = await repository.Add(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestUser", result.Username);
        }

        [Fact]
        public async Task FindAll_ShouldReturnAllUsers()
        {
            var context = TestDbContextFactory.Create();
            var repository = new UserRepository(context);
            await repository.Add(new User { Username = "User1", Password = "Test@1234", Role = "Admin" });
            await repository.Add(new User { Username = "User2", Password = "Test@1234", Role = "User" });

            var result = await repository.FindAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindById_ShouldReturnCorrectUser()
        {
            var context = TestDbContextFactory.Create();
            var repository = new UserRepository(context);
            var added = await repository.Add(new User { Username = "User1", Password = "Test@1234", Role = "Admin" });

            var result = await repository.FindById(added.Id);

            Assert.NotNull(result);
            Assert.Equal("User1", result.Username);
        }

        [Fact]
        public async Task FindById_ShouldReturnNull_WhenNotFound()
        {
            var context = TestDbContextFactory.Create();
            var repository = new UserRepository(context);

            var result = await repository.FindById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldModifyUser()
        {
            var context = TestDbContextFactory.Create();
            var repository = new UserRepository(context);
            var added = await repository.Add(new User { Username = "OldUser", Password = "Test@1234", Role = "Admin" });

            var updated = await repository.Update(added.Id, new User { Username = "NewUser", Password = "Test@1234", Role = "Admin" });

            Assert.NotNull(updated);
            Assert.Equal("NewUser", updated.Username);
        }

        [Fact]
        public async Task Delete_ShouldRemoveUser()
        {
            var context = TestDbContextFactory.Create();
            var repository = new UserRepository(context);
            var added = await repository.Add(new User { Username = "User1", Password = "Test@1234", Role = "Admin" });

            var result = await repository.Delete(added.Id);

            Assert.True(result);
            Assert.Null(await repository.FindById(added.Id));
        }
    }
}