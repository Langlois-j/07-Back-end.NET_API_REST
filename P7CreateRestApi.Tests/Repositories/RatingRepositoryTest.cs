using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Tests
{
    public class RatingRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddRating()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RatingRepository(context);
            var rating = new Rating { MoodysRating = "Aaa", SandPRating = "AAA" };

            var result = await repository.Add(rating);

            Assert.NotNull(result);
            Assert.Equal("Aaa", result.MoodysRating);
        }

        [Fact]
        public async Task FindAll_ShouldReturnAllRatings()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RatingRepository(context);
            await repository.Add(new Rating { MoodysRating = "Aaa" });
            await repository.Add(new Rating { MoodysRating = "Bbb" });

            var result = await repository.FindAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindById_ShouldReturnCorrectRating()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RatingRepository(context);
            var added = await repository.Add(new Rating { MoodysRating = "Aaa" });

            var result = await repository.FindById(added.Id);

            Assert.NotNull(result);
            Assert.Equal("Aaa", result.MoodysRating);
        }

        [Fact]
        public async Task FindById_ShouldReturnNull_WhenNotFound()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RatingRepository(context);

            var result = await repository.FindById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldModifyRating()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RatingRepository(context);
            var added = await repository.Add(new Rating { MoodysRating = "OldRating" });

            var updated = await repository.Update(added.Id, new Rating { MoodysRating = "NewRating" });

            Assert.NotNull(updated);
            Assert.Equal("NewRating", updated.MoodysRating);
        }

        [Fact]
        public async Task Delete_ShouldRemoveRating()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RatingRepository(context);
            var added = await repository.Add(new Rating { MoodysRating = "Aaa" });

            var result = await repository.Delete(added.Id);

            Assert.True(result);
            Assert.Null(await repository.FindById(added.Id));
        }
    }
}