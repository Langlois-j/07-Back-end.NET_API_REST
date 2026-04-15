using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace P7CreateRestApi.Tests.Controllers
{
    public class UserControllerTests
    {
        private static (UserController controller, UserRepository repo, UserManager<User> manager)
            CreateController()
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new LocalDbContext(options);
            var store = new UserStore<User>(context);

            var manager = new UserManager<User>(
                store,
                Options.Create(new IdentityOptions()),
                new PasswordHasher<User>(),
                new List<IUserValidator<User>> { new UserValidator<User>() },
                new List<IPasswordValidator<User>> { new PasswordValidator<User>() },
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null,
                new Logger<UserManager<User>>(new LoggerFactory())
            );

            var repo = new UserRepository(manager);
            var mapper = new UserMapper();
            return (new UserController(repo, mapper), repo, manager);
        }

        // ── GetAll ──────────────────────────────────────────────────────────────

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithAllUsers()
        {
            var (controller, repo, _) = CreateController();
            await repo.Add(new User { UserName = "user1", Role = "User" }, "Password123!");
            await repo.Add(new User { UserName = "user2", Role = "Admin" }, "Password123!");

            var result = controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<UserDTO>>(ok.Value);
            Assert.Equal(2, list.Count());
        }

        // ── GetById ─────────────────────────────────────────────────────────────

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenFound()
        {
            var (controller, repo, manager) = CreateController();
            await repo.Add(new User { UserName = "jdupont", Role = "User" }, "Password123!");
            var user = await manager.FindByNameAsync("jdupont");

            var result = await controller.GetById(user!.Id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<UserDTO>(ok.Value);
            Assert.Equal("jdupont", dto.UserName);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenMissing()
        {
            var (controller, _, _) = CreateController();

            var result = await controller.GetById("id-inconnu");

            Assert.IsType<NotFoundResult>(result);
        }

        // ── Validate (POST) ─────────────────────────────────────────────────────

        [Fact]
        public async Task Validate_ShouldReturnCreated_WhenModelIsValid()
        {
            var (controller, _, _) = CreateController();
            var dto = new UserCreateDTO
            {
                UserName = "nouveluser",
                Password = "Password123!",
                Fullname = "Nouvel User",
                Role = "User"
            };

            var result = await controller.Validate(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<UserDTO>(created.Value);
            Assert.Equal("nouveluser", returned.UserName);
        }

        [Fact]
        public async Task Validate_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            var (controller, _, _) = CreateController();
            controller.ModelState.AddModelError("UserName", "Requis");

            var result = await controller.Validate(new UserCreateDTO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Validate_ShouldReturnBadRequest_WhenPasswordTooWeak()
        {
            var (controller, _, _) = CreateController();
            var dto = new UserCreateDTO
            {
                UserName = "testuser",
                Password = "123",
                Role = "User"
            };

            var result = await controller.Validate(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // ── UpdateUser ──────────────────────────────────────────────────────────

        [Fact]
        public async Task UpdateUser_ShouldReturnOk_WhenFound()
        {
            var (controller, repo, manager) = CreateController();
            await repo.Add(new User { UserName = "avant", Role = "User" }, "Password123!");
            var user = await manager.FindByNameAsync("avant");

            var dto = new UserDTO { UserName = "apres", Fullname = "Nom Modifié", Role = "Admin" };
            var result = await controller.UpdateUser(user!.Id, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<UserDTO>(ok.Value);
            Assert.Equal("apres", returned.UserName);
            Assert.Equal("Admin", returned.Role);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnNotFound_WhenMissing()
        {
            var (controller, _, _) = CreateController();

            var result = await controller.UpdateUser("id-inconnu", new UserDTO());

            Assert.IsType<NotFoundResult>(result);
        }

        // ── DeleteUser ──────────────────────────────────────────────────────────

        [Fact]
        public async Task DeleteUser_ShouldReturnNoContent_WhenFound()
        {
            var (controller, repo, manager) = CreateController();
            await repo.Add(new User { UserName = "asupprimer", Role = "User" }, "Password123!");
            var user = await manager.FindByNameAsync("asupprimer");

            var result = await controller.DeleteUser(user!.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNotFound_WhenMissing()
        {
            var (controller, _, _) = CreateController();

            var result = await controller.DeleteUser("id-inconnu");

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
