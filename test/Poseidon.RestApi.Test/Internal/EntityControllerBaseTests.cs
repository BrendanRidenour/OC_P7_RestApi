using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Bids;
using Poseidon.RestApi.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Internal
{
    public class EntityControllerBaseTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(ControllerBase)
                .IsAssignableFrom(typeof(EntityControllerBase<BidEntity>)));
        }

        [Fact]
        public void HasApiControllerAttribute()
        {
            var attribute = GetClassAttribute<EntityControllerBase<BidEntity>, ApiControllerAttribute>();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void HasRouteAttribute()
        {
            var attribute = GetClassAttribute<EntityControllerBase<BidEntity>, RouteAttribute>();

            Assert.NotNull(attribute);
            Assert.Equal("[controller]", attribute.Template);
        }

        [Fact]
        public void Constructor_NullCrudStore_Throws()
        {
            Assert.Throws<ArgumentNullException>("crudStore", () =>
            {
                new TestEntityControllerBase(crudStore: null!);
            });
        }

        [Fact]
        public void Create_HasHttpPostAttribute()
        {
            var attribute = GetMethodAttribute<EntityControllerBase<BidEntity>, HttpPostAttribute>(
                "Create");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Create_EntityParameterHasFromBodyAttribute()
        {
            var attribute = GetParameterAttribute<EntityControllerBase<BidEntity>, FromBodyAttribute>(
                "Create", "entity");

            Assert.NotNull(attribute);
        }

        [Fact]
        public async Task Create_WhenCalled_CallsCreateOnCrudStore()
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);
            var entity = CreateEntity();

            await controller.Create(entity);

            Assert.Equal(crudStore.Create_InputEntity, entity);
        }

        [Fact]
        public async Task Create_WhenCalled_ReturnsEntityFromCrudStore()
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);
            var entity = CreateEntity();

            var result = await controller.Create(entity);

            Assert.Equal(crudStore.Create_Result, result.Value);
        }

        [Fact]
        public void Read_HasHttpGetAttribute()
        {
            var attribute = GetMethodAttribute<EntityControllerBase<BidEntity>, HttpGetAttribute>(
                "Read");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Read_HasRouteAttribute()
        {
            var attribute = GetMethodAttribute<EntityControllerBase<BidEntity>, RouteAttribute>(
                "Read");

            Assert.NotNull(attribute);
            Assert.Equal("{id}", attribute.Template);
        }

        [Fact]
        public void Read_IdParameterHasFromRouteAttribute()
        {
            var attribute = GetParameterAttribute<EntityControllerBase<BidEntity>, FromRouteAttribute>(
                "Read", "id");

            Assert.NotNull(attribute);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Read_WhenCalled_CallsReadOnCrudStore(int id)
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);

            await controller.Read(id);

            Assert.Equal(crudStore.Read_InputId, id);
        }

        [Fact]
        public async Task Read_CrudStoreReturnsNull_ReturnsNotFound()
        {
            var crudStore = CrudStore();
            crudStore.Read_Result = null;
            var controller = Controller(crudStore);

            var actionResult = await controller.Read(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task Read_CrudStoreReturnsEntity_ReturnsEntity()
        {
            var crudStore = CrudStore();
            crudStore.Read_Result = CreateEntity();
            var controller = Controller(crudStore);

            var actionResult = await controller.Read(id: 1);

            Assert.Equal(crudStore.Read_Result, actionResult.Value);
        }

        [Fact]
        public void Update_HasHttpPutAttribute()
        {
            var attribute = GetMethodAttribute<EntityControllerBase<BidEntity>, HttpPutAttribute>(
                "Update");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Update_HasRouteAttribute()
        {
            var attribute = GetMethodAttribute<EntityControllerBase<BidEntity>, RouteAttribute>(
                "Update");

            Assert.NotNull(attribute);
            Assert.Equal("{id}", attribute.Template);
        }

        [Fact]
        public void Update_IdParameterHasFromRouteAttribute()
        {
            var attribute = GetParameterAttribute<EntityControllerBase<BidEntity>, FromRouteAttribute>(
                "Update", "id");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Update_EntityParameterHasFromBodyAttribute()
        {
            var attribute = GetParameterAttribute<EntityControllerBase<BidEntity>, FromBodyAttribute>(
                "Update", "entity");

            Assert.NotNull(attribute);
        }

        [Fact]
        public async Task Update_IdDoesNotMatchEntityId_ReturnsBadRequest()
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);
            var entity = CreateEntity();
            entity.Id = 2;

            var result = await controller.Update(id: 1, entity);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Update_WhenCalled_CallsUpdateOnCrudStore()
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);
            var entity = CreateEntity();

            await controller.Update(id: 1, entity);

            Assert.Equal(entity, crudStore.Update_InputEntity);
        }

        [Fact]
        public async Task Update_WhenCalled_ReturnsEntityFromCrudStore()
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);
            var entity = CreateEntity();

            var result = await controller.Update(id: 1, entity);

            Assert.Equal(crudStore.Update_Result, result.Value);
        }

        [Fact]
        public void Delete_HasHttpDeleteAttribute()
        {
            var attribute = GetMethodAttribute<EntityControllerBase<BidEntity>, HttpDeleteAttribute>(
                "Delete");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Delete_HasRouteAttribute()
        {
            var attribute = GetMethodAttribute<EntityControllerBase<BidEntity>, RouteAttribute>(
                "Delete");

            Assert.NotNull(attribute);
            Assert.Equal("{id}", attribute.Template);
        }

        [Fact]
        public void Delete_IdParameterHasFromRouteAttribute()
        {
            var attribute = GetParameterAttribute<EntityControllerBase<BidEntity>, FromRouteAttribute>(
                "Delete", "id");

            Assert.NotNull(attribute);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Delete_WhenCalled_CallsDeleteOnCrudStore(int id)
        {
            var crudStore = CrudStore();
            var controller = Controller(crudStore);

            await controller.Delete(id);

            Assert.Equal(id, crudStore.Delete_InputId);
        }

        private static MockCrudStore CrudStore() => new MockCrudStore();
        private static TestEntityControllerBase Controller(MockCrudStore? crudStore = null) =>
            new TestEntityControllerBase(crudStore ?? CrudStore());
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

        private class TestEntityControllerBase : EntityControllerBase<BidEntity>
        {
            new public MockCrudStore CrudStore => (MockCrudStore)base.CrudStore;

            public TestEntityControllerBase(MockCrudStore crudStore)
                : base(crudStore)
            { }
        }
    }
}