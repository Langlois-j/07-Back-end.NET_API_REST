using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Repositories
{
    public class CurvePointRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddCurvePoint()
        {
            var context = TestDbContextFactory.Create();
            var repository = new CurvePointRepository(context);
            var curvePoint = new CurvePoint { CurveId = 1, Term = 1.5 };

            var result = await repository.Add(curvePoint);

            Assert.NotNull(result);
            Assert.Equal(1.5, result.Term);
        }

        [Fact]
        public async Task FindAll_ShouldReturnAllCurvePoints()
        {
            var context = TestDbContextFactory.Create();
            var repository = new CurvePointRepository(context);
            await repository.Add(new CurvePoint { CurveId = 1 });
            await repository.Add(new CurvePoint { CurveId = 2 });

            var result = await repository.FindAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindById_ShouldReturnCorrectCurvePoint()
        {
            var context = TestDbContextFactory.Create();
            var repository = new CurvePointRepository(context);
            var added = await repository.Add(new CurvePoint { CurveId = 1 });

            var result = await repository.FindById(added.Id);

            Assert.NotNull(result);
            Assert.Equal((byte)1, result.CurveId);
        }

        [Fact]
        public async Task FindById_ShouldReturnNull_WhenNotFound()
        {
            var context = TestDbContextFactory.Create();
            var repository = new CurvePointRepository(context);

            var result = await repository.FindById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldModifyCurvePoint()
        {
            var context = TestDbContextFactory.Create();
            var repository = new CurvePointRepository(context);
            var added = await repository.Add(new CurvePoint { Term = 1.0 });

            var updated = await repository.Update(added.Id, new CurvePoint { Term = 2.0 });

            Assert.NotNull(updated);
            Assert.Equal(2.0, updated.Term);
        }

        [Fact]
        public async Task Delete_ShouldRemoveCurvePoint()
        {
            var context = TestDbContextFactory.Create();
            var repository = new CurvePointRepository(context);
            var added = await repository.Add(new CurvePoint { CurveId = 1 });

            var result = await repository.Delete(added.Id);

            Assert.True(result);
            Assert.Null(await repository.FindById(added.Id));
        }
    }
}