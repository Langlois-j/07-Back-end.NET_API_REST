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
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Controllers
{
    public class UserControllerTests
    {
        private static (UserController controller, UserRepository repo,UserManager<User> manager, RoleManager<IdentityRole> roleManager)
       CreateController()
        {
            var context = TestDbContextFactory.Create();
            var (manager, roleManager) = IdentityTestFactory.Create(context);

            var repo = new UserRepository(manager);
            var mapper = new UserMapper();
            var logger = new Microsoft.Extensions.Logging.Abstractions.NullLogger<UserController>();
            return (new UserController(repo, mapper, logger), repo, manager, roleManager);
        }       

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithAllUsers()
        {
            var (controller, repo, _, _) = CreateController();
            await repo.Add(new User { UserName = "user1"}, "Password123!");
            await repo.Add(new User { UserName = "user2" }, "Password123!");

            var result = await controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<UserDTO>>(ok.Value);
            Assert.Equal(2, list.Count());
        }

      

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenFound()
        {
            var (controller, repo, manager, _) = CreateController();
            await repo.Add(new User { UserName = "jdupont"}, "Password123!");
            var user = await manager.FindByNameAsync("jdupont");

            var result = await controller.GetById(user!.Id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<UserDTO>(ok.Value);
            Assert.Equal("jdupont", dto.UserName);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenMissing()
        {
            var (controller, repo, _, _) = CreateController();

            var result = await controller.GetById("id-inconnu");

            Assert.IsType<NotFoundResult>(result);
        }

        

        [Fact]
        public async Task Validate_ShouldReturnCreated_WhenModelIsValid()
        {
            var (controller, _, _, roleManager) = CreateController();
            await IdentityTestFactory.SeedRolesAsync(roleManager);

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
            var (controller, repo, _, _) = CreateController();
            controller.ModelState.AddModelError("UserName", "Requis");

            var result = await controller.Validate(new UserCreateDTO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Validate_ShouldReturnBadRequest_WhenPasswordTooWeak()
        {
            var (controller, repo, _, _) = CreateController();
            var dto = new UserCreateDTO
            {
                UserName = "testuser",
                Password = "123",
                Role = "User"
            };

            var result = await controller.Validate(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

   

        [Fact]
        public async Task UpdateUser_ShouldReturnOk_WhenFound()
        {
            var (controller, repo, manager, roleManager) = CreateController(); 
            await IdentityTestFactory.SeedRolesAsync(roleManager);

            await repo.Add(new User { UserName = "avant"}, "Password123!");
            var user = await manager.FindByNameAsync("avant");

            var dto = new UserDTO { UserName = "apres", Fullname = "Nom Modifié", Role = "Admin" };
            var result = await controller.UpdateUser(user!.Id, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<UserDTO>(ok.Value);
            Assert.Equal("apres", returned.UserName);
           
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnNotFound_WhenMissing()
        {
            var (controller, repo, _, _) = CreateController();

            var result = await controller.UpdateUser("id-inconnu", new UserDTO());

            Assert.IsType<NotFoundResult>(result);
        }

     

        [Fact]
        public async Task DeleteUser_ShouldReturnNoContent_WhenFound()
        {
            var (controller, repo, manager, _) = CreateController();
            await repo.Add(new User { UserName = "asupprimer"}, "Password123!");
            var user = await manager.FindByNameAsync("asupprimer");

            var result = await controller.DeleteUser(user!.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNotFound_WhenMissing()
        {
            var (controller, _, _, _) = CreateController();

            var result = await controller.DeleteUser("id-inconnu");

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
