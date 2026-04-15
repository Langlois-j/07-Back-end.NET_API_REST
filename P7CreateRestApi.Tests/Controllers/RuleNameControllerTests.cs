using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Controllers
{
    public class RuleNameControllerTests
    {
        private static RuleNameController CreateController(out FakeRepository<RuleName> repo)
        {
            repo = new FakeRepository<RuleName>();
            var mapper = new FakeMapper<RuleName, RuleNameDTO>();
            return new RuleNameController(repo, mapper);
        }

        // ── Home ────────────────────────────────────────────────────────────────

        [Fact]
        public async Task Home_ShouldReturnOk_WithAllRuleNames()
        {
            var controller = CreateController(out var repo);
            await repo.Add(new RuleName { Name = "Rule1" });
            await repo.Add(new RuleName { Name = "Rule2" });

            var result = await controller.Home();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<RuleNameDTO>>(ok.Value);
            Assert.Equal(2, list.Count());
        }

        // ── GetById ─────────────────────────────────────────────────────────────

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new RuleName { Name = "RuleX" });

            var result = await controller.GetById(added.Id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<RuleNameDTO>(ok.Value);
            Assert.Equal("RuleX", dto.Name);
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
            var dto = new RuleNameDTO { Name = "NewRule", Description = "Desc" };

            var result = await controller.Validate(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<RuleNameDTO>(created.Value);
            Assert.Equal("NewRule", returned.Name);
        }

        [Fact]
        public async Task Validate_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            var controller = CreateController(out _);
            controller.ModelState.AddModelError("Name", "Requis");

            var result = await controller.Validate(new RuleNameDTO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // ── UpdateRuleName ──────────────────────────────────────────────────────

        [Fact]
        public async Task UpdateRuleName_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new RuleName { Name = "OldRule" });

            var result = await controller.UpdateRuleName(added.Id, new RuleNameDTO { Name = "NewRule" });

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<RuleNameDTO>(ok.Value);
            Assert.Equal("NewRule", dto.Name);
        }

        [Fact]
        public async Task UpdateRuleName_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.UpdateRuleName(999, new RuleNameDTO());

            Assert.IsType<NotFoundResult>(result);
        }

        // ── DeleteRuleName ──────────────────────────────────────────────────────

        [Fact]
        public async Task DeleteRuleName_ShouldReturnNoContent_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new RuleName { Name = "Rule1" });

            var result = await controller.DeleteRuleName(added.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteRuleName_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.DeleteRuleName(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
