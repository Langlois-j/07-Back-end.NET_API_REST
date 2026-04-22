using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Controllers
{
    public class RatingControllerTests
    {
        private static RatingController CreateController(out FakeRepository<Rating> repo)
        {
            repo = new FakeRepository<Rating>();
            var mapper = new FakeMapper<Rating, RatingDTO>();
            return new RatingController(repo, mapper);
        }

      
        [Fact]
        public async Task Home_ShouldReturnOk_WithAllRatings()
        {
            var controller = CreateController(out var repo);
            await repo.Add(new Rating { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" });
            await repo.Add(new Rating { MoodysRating = "Baa", SandPRating = "BBB", FitchRating = "BBB" });

            var result = await controller.Home();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<RatingDTO>>(ok.Value);
            Assert.Equal(2, list.Count());
        }

       

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new Rating { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" });

            var result = await controller.GetById(added.Id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<RatingDTO>(ok.Value);
            Assert.Equal("Aaa", dto.MoodysRating);
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
            var dto = new RatingDTO { MoodysRating = "Aa1", SandPRating = "AA+", FitchRating = "AA+" };

            var result = await controller.Validate(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<RatingDTO>(created.Value);
            Assert.Equal("Aa1", returned.MoodysRating);
        }

        [Fact]
        public async Task Validate_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            var controller = CreateController(out _);
            controller.ModelState.AddModelError("MoodysRating", "Requis");

            var result = await controller.Validate(new RatingDTO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

    

        [Fact]
        public async Task UpdateRatingt_ShouldReturnOk_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new Rating { MoodysRating = "Old", SandPRating = "AAA", FitchRating = "AAA" });

            var result = await controller.UpdateRatingt(added.Id,
                new RatingDTO { MoodysRating = "New", SandPRating = "AAA", FitchRating = "AAA" });

            var ok = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<RatingDTO>(ok.Value);
            Assert.Equal("New", dto.MoodysRating);
        }

        [Fact]
        public async Task UpdateRatingt_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.UpdateRatingt(999, new RatingDTO());

            Assert.IsType<NotFoundResult>(result);
        }

      

        [Fact]
        public async Task DeleteRating_ShouldReturnNoContent_WhenFound()
        {
            var controller = CreateController(out var repo);
            var added = await repo.Add(new Rating { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" });

            var result = await controller.DeleteRating(added.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteRating_ShouldReturnNotFound_WhenMissing()
        {
            var controller = CreateController(out _);

            var result = await controller.DeleteRating(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
