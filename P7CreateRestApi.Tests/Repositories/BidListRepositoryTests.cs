using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using P7CreateRestApi.Tests.Helpers;

namespace P7CreateRestApi.Tests.Repositories
{
    public class BidListRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddBidList()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            var repository = new BidListRepository(context);
            var bidList = new BidList { Account = "TestAccount", BidType = "Type1" };

            // Act
            var result = await repository.Add(bidList);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestAccount", result.Account);
        }

        [Fact]
        public async Task FindAll_ShouldReturnAllBidLists()
        {
            var context = TestDbContextFactory.Create();
            var repository = new BidListRepository(context);
            await repository.Add(new BidList { Account = "Account1", BidType = "Type1" });
            await repository.Add(new BidList { Account = "Account2", BidType = "Type1" });

            var result = await repository.FindAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindById_ShouldReturnCorrectBidList()
        {
            var context = TestDbContextFactory.Create();
            var repository = new BidListRepository(context);
            var added = await repository.Add(new BidList { Account = "Account1", BidType = "Type1" });

            var result = await repository.FindById(added.Id);

            Assert.NotNull(result);
            Assert.Equal("Account1", result.Account);
        }

        [Fact]
        public async Task FindById_ShouldReturnNull_WhenNotFound()
        {
            var context = TestDbContextFactory.Create();
            var repository = new BidListRepository(context);

            var result = await repository.FindById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldModifyBidList()
        {
            var context = TestDbContextFactory.Create();
            var repository = new BidListRepository(context);
            var added = await repository.Add(new BidList { Account = "OldAccount", BidType = "Type1" });

            var updated = await repository.Update(added.Id, new BidList { Account = "NewAccount", BidType = "Type1" });

            Assert.NotNull(updated);
            Assert.Equal("NewAccount", updated.Account);
        }

        [Fact]
        public async Task Delete_ShouldRemoveBidList()
        {
            var context = TestDbContextFactory.Create();
            var repository = new BidListRepository(context);
            var added = await repository.Add(new BidList { Account = "Account1", BidType = "Type1" });

            var result = await repository.Delete(added.Id);

            Assert.True(result);
            Assert.Null(await repository.FindById(added.Id));
        }
    }
}