using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Controllers
{
    public class CurveControllerTests
    {
        private static CurveController CreateController(out FakeRepository<CurvePoint> repo)
        {
            repo = new FakeRepository<CurvePoint>();
            var mapper = new FakeMapper<CurvePoint, CurveDTO>();
            return new CurveController(repo, mapper);
        }

        // ── Home ────────────────────────────────────────────────────────────────

        [Fact]
        public async Task Home_ShouldReturnOk_WithAllCurvePoints()
        {
            var controller = CreateController(out var repo);
            await repo.Add(new CurvePoint { CurveId = 1, Term = 1.0 });
            await repo.Add(new CurvePoint { CurveId = 2, Term = 2.0 });

            var result = await controller.Home();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<CurveDTO>>(ok.Value);
            Assert.Equal(2, list.Count());
        }

        // ── GetById ─────────────────────────────────────────────────────────────

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new CurvePoint { CurveId = 5, Term = 3.5 });

            var result = await controller.GetById(added.Id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<CurveDTO>(ok.Value);
            Assert.Equal(3.5, dto.Term);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.GetById(999);

            Assert.IsType<NotFoundResult>(result);
        }

        // ── Validate ────────────────────────────────────────────────────────────

        [Fact]
        public async Task Validate_ShouldReturnCreated_WhenModelIsValid()
        {
            var controller = CreateController(out _);
            var dto = new CurveDTO { CurveId = 3, Term = 5.0, CurvePointValue = 100.0 };

            var result = await controller.Validate(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<CurveDTO>(created.Value);
            Assert.Equal(5.0, returned.Term);
        }

        [Fact]
        public async Task Validate_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            var controller = CreateController(out _);
            controller.ModelState.AddModelError("CurveId", "Requis");

            var result = await controller.Validate(new CurveDTO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // ── UpdateCurvePoint ────────────────────────────────────────────────────

        [Fact]
        public async Task UpdateCurvePoint_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new CurvePoint { Term = 1.0 });

            var result = await controller.UpdateCurvePoint(added.Id, new CurveDTO { Term = 9.9 });

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<CurveDTO>(ok.Value);
            Assert.Equal(9.9, dto.Term);
        }

        [Fact]
        public async Task UpdateCurvePoint_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.UpdateCurvePoint(999, new CurveDTO());

            Assert.IsType<NotFoundResult>(result);
        }

        // ── DeleteBid ───────────────────────────────────────────────────────────

        [Fact]
        public async Task DeleteBid_ShouldReturnNoContent_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new CurvePoint { CurveId = 1 });

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
