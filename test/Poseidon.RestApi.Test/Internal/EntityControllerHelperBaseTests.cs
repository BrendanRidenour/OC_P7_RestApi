using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Bids;
using Poseidon.RestApi.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Poseidon.RestApi.Internal
{
    public class EntityControllerHelperBaseTests
    {
        [Fact]
        public void InheritsControllerBase()
        {
            Assert.True(typeof(ControllerBase)
                .IsAssignableFrom(typeof(EntityControllerHelperBase<BidEntity>)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task CreateEntity_EntityIsSet_SetsEntityIdToDefault(int id)
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);
            var entity = CreateEntity(id);

            await controller.CreateEntity(entity);

            Assert.NotEqual(id, crudStore.Create_InputEntity!.Id);
            Assert.Equal(default, crudStore.Create_InputEntity.Id);
        }

        [Fact]
        public async Task CreateEntity_WhenCalled_CallsCreateOnCrudStore()
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);
            var entity = CreateEntity();

            await controller.CreateEntity(entity);

            Assert.Equal(crudStore.Create_InputEntity, entity);
        }

        [Theory]
        [InlineData("TestRead1")]
        [InlineData("TestRead2")]
        public async Task CreateEntity_WhenCalled_ReturnsCreatedAtActionResult(
            string readEntityActionName)
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore, readEntityActionName);
            var entity = CreateEntity();

            var actionResult = await controller.CreateEntity(entity);

            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(readEntityActionName, createdResult.ActionName);
            Assert.Equal(entity.Id, createdResult.RouteValues!["id"]);
            Assert.Equal(crudStore.Create_Result, createdResult.Value);
        }

        [Fact]
        public async Task ReadEntities_WhenCalled_CallsReadOnCrudStore()
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);

            var results = await controller.ReadEntities();

            Assert.True(crudStore.ReadList_Called);
            Assert.Equal(crudStore.ReadList_Result, results);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task ReadEntity_WhenCalled_CallsReadOnCrudStore(int id)
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);

            await controller.ReadEntity(id);

            Assert.Equal(crudStore.Read_InputId, id);
        }

        [Fact]
        public async Task ReadEntity_CrudStoreReturnsNull_ReturnsNotFound()
        {
            var crudStore = CrudStore();
            crudStore.Read_Result = null;
            var controller = Controller(crudStore);

            var actionResult = await controller.ReadEntity(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task ReadEntity_CrudStoreReturnsEntity_ReturnsEntity()
        {
            var crudStore = CrudStore();
            crudStore.Read_Result = CreateEntity();
            var controller = Controller(crudStore);

            var actionResult = await controller.ReadEntity(id: 1);

            Assert.Equal(crudStore.Read_Result, actionResult.Value);
        }

        [Fact]
        public async Task UpdateEntity_CallToReadOnCrudStoreReturnsNull_ReturnsNotFound()
        {
            var crudStore = CrudStore();
            crudStore.Read_Result = null;
            var controller = Controller(crudStore);
            var entity = CreateEntity();

            var actionResult = await controller.UpdateEntity(id: 1, entity);

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task UpdateEntity_WhenCalled_CallsUpdateOnCrudStoreWithCorrectId(int id)
        {
            var crudStore = CrudStore();
            crudStore.Read_Result!.Id = id;
            var controller = Controller(crudStore);
            var entity = CreateEntity(id: 0);

            await controller.UpdateEntity(id, entity);

            Assert.Equal(crudStore.Read_Result, crudStore.Update_InputEntity);
            Assert.Equal(id, crudStore.Update_InputEntity!.Id);
        }

        [Fact]
        public async Task UpdateEntity_WhenCalled_ReturnsNoContent()
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);
            var entity = CreateEntity();

            var result = await controller.UpdateEntity(id: 1, entity);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteEntity_ReadOnCrudStoreReturnsNull_ReturnsNotFound()
        {
            var crudStore = CrudStore();
            crudStore.Read_Result = null;
            var controller = Controller(crudStore);

            var result = await controller.DeleteEntity(id: 1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DeleteEntity_WhenCalled_CallsDeleteOnCrudStore(int id)
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);

            await controller.DeleteEntity(id);

            Assert.Equal(id, crudStore.Delete_InputId);
        }

        [Fact]
        public async Task DeleteEntity_WhenCalled_ReturnsNoContent()
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);

            var result = await controller.DeleteEntity(id: 1);

            Assert.IsType<NoContentResult>(result);
        }

        private static MockBidEntityCrudStore CrudStore() =>
            new MockBidEntityCrudStore();
        private static TestEntityControllerHelperBase Controller(
            MockBidEntityCrudStore? crudStore = null,
            string readEntityActionName = "TestRead") =>
            new TestEntityControllerHelperBase(crudStore ?? CrudStore(),
                readEntityActionName);
        private static BidEntity CreateEntity(
            int id = 1,
            string account = "Test Account",
            string type = "Test Type",
            double bidQuantity = 1,
            double askQuantity = 1,
            double bid = 1,
            double ask = 1,
            string benchmark = "Test Benchmark",
            DateTimeOffset? bidListDate = null,
            string commentary = "Test Commentary",
            string security = "Test Security",
            string status = "TestStatus",
            string trader = "Test Trader",
            string book = "Test Book",
            string creationName = "Test Creation Name",
            DateTimeOffset? creationDate = null,
            string revisionName = "Test Revision Name",
            DateTimeOffset? revisionDate = null,
            string dealName = "Test Deal Name",
            string dealType = "Test Deal Type",
            string sourceListId = "TestSourceListId",
            string side = "Test Side") =>
            new BidEntity()
            {
                Id = id,
                Account = account,
                Type = type,
                BidQuantity = bidQuantity,
                AskQuantity = askQuantity,
                Bid = bid,
                Ask = ask,
                Benchmark = benchmark,
                BidListDate = bidListDate ?? DateTimeOffset.UtcNow,
                Commentary = commentary,
                Security = security,
                Status = status,
                Trader = trader,
                Book = book,
                CreationName = creationName,
                CreationDate = creationDate ?? DateTimeOffset.UtcNow,
                RevisionName = revisionName,
                RevisionDate = revisionDate ?? DateTimeOffset.UtcNow,
                DealName = dealName,
                DealType = dealType,
                SourceListId = sourceListId,
                Side = side,
            };

        private class TestEntityControllerHelperBase
            : EntityControllerHelperBase<BidEntity>
        {
            new public MockBidEntityCrudStore CrudStore => (MockBidEntityCrudStore)base.CrudStore;

            protected override string ReadEntityActionName { get; }

            public TestEntityControllerHelperBase(MockBidEntityCrudStore crudStore,
                string readEntityActionName = "TestRead")
                : base(crudStore)
            {
                this.ReadEntityActionName = readEntityActionName;
            }

            new public Task<ActionResult<BidEntity>> CreateEntity(BidEntity entity) =>
                base.CreateEntity(entity);

            new public Task<IEnumerable<BidEntity>> ReadEntities() =>
                base.ReadEntities();

            new public Task<ActionResult<BidEntity?>> ReadEntity(int id) =>
                base.ReadEntity(id);

            new public Task<ActionResult> UpdateEntity(int id, BidEntity entity) =>
                base.UpdateEntity(id, entity);

            new public Task<ActionResult> DeleteEntity(int id) =>
                base.DeleteEntity(id);
        }
    }
}