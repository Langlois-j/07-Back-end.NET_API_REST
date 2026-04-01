using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Tests
{
    public class TradeRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddTrade()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            var repository = new TradeRepository(context);
            var trade = new Trade { Account = "TestAccount", AccountType = "Type1" };

            // Act
            var result = await repository.Add(trade);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestAccount", result.Account);
        }

        [Fact]
        public async Task FindAll_ShouldReturnAllTrades()
        {
            var context = TestDbContextFactory.Create();
            var repository = new TradeRepository(context);
            await repository.Add(new Trade { Account = "Account1", AccountType = "Type1" });
            await repository.Add(new Trade { Account = "Account2", AccountType = "Type1" });

            var result = await repository.FindAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindById_ShouldReturnCorrectTrade()
        {
            var context = TestDbContextFactory.Create();
            var repository = new TradeRepository(context);
            var added = await repository.Add(new Trade { Account = "Account1", AccountType = "Type1" });

            var result = await repository.FindById(added.Id);

            Assert.NotNull(result);
            Assert.Equal("Account1", result.Account);
        }

        [Fact]
        public async Task FindById_ShouldReturnNull_WhenNotFound()
        {
            var context = TestDbContextFactory.Create();
            var repository = new TradeRepository(context);

            var result = await repository.FindById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldModifyTrade()
        {
            var context = TestDbContextFactory.Create();
            var repository = new TradeRepository(context);
            var added = await repository.Add(new Trade { Account = "OldAccount", AccountType = "Type1" });

            var updated = await repository.Update(added.Id, new Trade { Account = "NewAccount", AccountType = "Type1" });

            Assert.NotNull(updated);
            Assert.Equal("NewAccount", updated.Account);
        }

        [Fact]
        public async Task Delete_ShouldRemoveTrade()
        {
            var context = TestDbContextFactory.Create();
            var repository = new TradeRepository(context);
            var added = await repository.Add(new Trade { Account = "Account1", AccountType = "Type1" });

            var result = await repository.Delete(added.Id);

            Assert.True(result);
            Assert.Null(await repository.FindById(added.Id));
        }
    }
}