using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Controllers
{
    public class BidListControllerTests
    {
        private static BidListController CreateController(
            out FakeRepository<BidList> repo)
        {
            repo = new FakeRepository<BidList>();
            var mapper = new FakeMapper<BidList, BidListDTO>();
            return new BidListController(repo, mapper);
        }

        // ── Home (GET /list) ────────────────────────────────────────────────────

        [Fact]
        public async Task Home_ShouldReturnOk_WithAllBidLists()
        {
            var controller = CreateController(out var repo);
            await repo.Add(new BidList { Account = "A1", BidType = "T1" });
            await repo.Add(new BidList { Account = "A2", BidType = "T2" });

            var result = await controller.Home();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<BidListDTO>>(ok.Value);
            Assert.Equal(2, list.Count());
        }

        [Fact]
        public async Task Home_ShouldReturnOk_WithEmptyList_WhenNoData()
        {
            var controller = CreateController(out _);

            var result = await controller.Home();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<BidListDTO>>(ok.Value);
            Assert.Empty(list);
        }

        // ── GetById ─────────────────────────────────────────────────────────────

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new BidList { Account = "A1", BidType = "T1" });

            var result = await controller.GetById(added.Id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<BidListDTO>(ok.Value);
            Assert.Equal("A1", dto.Account);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.GetById(999);

            Assert.IsType<NotFoundResult>(result);
        }

        // ── Validate (POST) ─────────────────────────────────────────────────────

        [Fact]
        public async Task Validate_ShouldReturnCreated_WhenModelIsValid()
        {
            var controller = CreateController(out _);
            var dto = new BidListDTO { Account = "NewAccount", BidType = "T1" };

            var result = await controller.Validate(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<BidListDTO>(created.Value);
            Assert.Equal("NewAccount", returned.Account);
        }

        [Fact]
        public async Task Validate_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            var controller = CreateController(out _);
            controller.ModelState.AddModelError("Account", "Requis");

            var result = await controller.Validate(new BidListDTO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // ── UpdateBid (PUT) ─────────────────────────────────────────────────────

        [Fact]
        public async Task UpdateBid_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new BidList { Account = "Old", BidType = "T1" });

            var result = await controller.UpdateBid(added.Id, new BidListDTO { Account = "New", BidType = "T1" });

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<BidListDTO>(ok.Value);
            Assert.Equal("New", dto.Account);
        }

        [Fact]
        public async Task UpdateBid_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.UpdateBid(999, new BidListDTO());

            Assert.IsType<NotFoundResult>(result);
        }

        // ── DeleteBid (DELETE) ──────────────────────────────────────────────────

        [Fact]
        public async Task DeleteBid_ShouldReturnNoContent_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new BidList { Account = "A1", BidType = "T1" });

            var result = await controller.DeleteBid(added.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBid_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.DeleteBid(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
