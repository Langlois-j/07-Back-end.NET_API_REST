using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Repositories
{
    public class RuleNameRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddRuleName()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RuleNameRepository(context);
            var ruleName = new RuleName { Name = "TestRule", Description = "TestDescription" };

            var result = await repository.Add(ruleName);

            Assert.NotNull(result);
            Assert.Equal("TestRule", result.Name);
        }

        [Fact]
        public async Task FindAll_ShouldReturnAllRuleNames()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RuleNameRepository(context);
            await repository.Add(new RuleName { Name = "Rule1" });
            await repository.Add(new RuleName { Name = "Rule2" });

            var result = await repository.FindAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindById_ShouldReturnCorrectRuleName()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RuleNameRepository(context);
            var added = await repository.Add(new RuleName { Name = "Rule1" });

            var result = await repository.FindById(added.Id);

            Assert.NotNull(result);
            Assert.Equal("Rule1", result.Name);
        }

        [Fact]
        public async Task FindById_ShouldReturnNull_WhenNotFound()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RuleNameRepository(context);

            var result = await repository.FindById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldModifyRuleName()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RuleNameRepository(context);
            var added = await repository.Add(new RuleName { Name = "OldName" });

            var updated = await repository.Update(added.Id, new RuleName { Name = "NewName" });

            Assert.NotNull(updated);
            Assert.Equal("NewName", updated.Name);
        }

        [Fact]
        public async Task Delete_ShouldRemoveRuleName()
        {
            var context = TestDbContextFactory.Create();
            var repository = new RuleNameRepository(context);
            var added = await repository.Add(new RuleName { Name = "Rule1" });

            var result = await repository.Delete(added.Id);

            Assert.True(result);
            Assert.Null(await repository.FindById(added.Id));
        }
    }
}