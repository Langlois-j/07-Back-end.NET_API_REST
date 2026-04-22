using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Controllers
{
    public class TradeControllerTests
    {
        private static TradeController CreateController(out FakeRepository<Trade> repo)
        {
            repo = new FakeRepository<Trade>();
            var mapper = new FakeMapper<Trade, TradeDTO>();
            return new TradeController(repo, mapper);
        }

    

        [Fact]
        public async Task Home_ShouldReturnOk_WithAllTrades()
        {
            var controller = CreateController(out var repo);
            await repo.Add(new Trade { Account = "A1", AccountType = "T1" });
            await repo.Add(new Trade { Account = "A2", AccountType = "T2" });

            var result = await controller.Home();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<TradeDTO>>(ok.Value);
            Assert.Equal(2, list.Count());
        }

      

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new Trade { Account = "AccountX", AccountType = "T1" });

            var result = await controller.GetById(added.Id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<TradeDTO>(ok.Value);
            Assert.Equal("AccountX", dto.Account);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.GetById(999);

            Assert.IsType<NotFoundResult>(result);
        }

     

        [Fact]
        public async Task Validate_ShouldReturnCreated_WhenModelIsValid()
        {
            var controller = CreateController(out _);
            var dto = new TradeDTO { Account = "NewAccount", AccountType = "Buy" };

            var result = await controller.Validate(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<TradeDTO>(created.Value);
            Assert.Equal("NewAccount", returned.Account);
        }

        [Fact]
        public async Task Validate_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            var controller = CreateController(out _);
            controller.ModelState.AddModelError("Account", "Requis");

            var result = await controller.Validate(new TradeDTO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

       

        [Fact]
        public async Task UpdateTrade_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new Trade { Account = "Old", AccountType = "T1" });

            var result = await controller.UpdateTrade(added.Id, new TradeDTO { Account = "New", AccountType = "T1" });

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<TradeDTO>(ok.Value);
            Assert.Equal("New", dto.Account);
        }

        [Fact]
        public async Task UpdateTrade_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.UpdateTrade(999, new TradeDTO());

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task DeleteTrade_ShouldReturnNoContent_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new Trade { Account = "A1", AccountType = "T1" });

            var result = await controller.DeleteTrade(added.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTrade_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.DeleteTrade(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
