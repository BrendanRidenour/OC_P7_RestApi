using Poseidon.RestApi.Bids;
using Poseidon.RestApi.Data;
using System;
using System.Threading.Tasks;

namespace Poseidon.RestApi.Mocks
{
    public class MockCrudStore : ICrudStore<BidEntity>
    {
        public BidEntity? Create_InputEntity;
        public BidEntity? Create_Result;
        public Task<BidEntity> Create(BidEntity entity)
        {
            this.Create_InputEntity = entity;
            this.Create_Result = entity;

            return Task.FromResult(entity);
        }

        public int Read_InputId;
        public BidEntity? Read_Result = CreateEntity();
        public Task<BidEntity?> Read(int id)
        {
            this.Read_InputId = id;

            return Task.FromResult(this.Read_Result);
        }

        public BidEntity? Update_InputEntity;
        public BidEntity? Update_Result;
        public Task<BidEntity> Update(BidEntity entity)
        {
            this.Update_InputEntity = entity;

            this.Update_Result = entity;

            return Task.FromResult(this.Update_Result);
        }

        public int Delete_InputId;
        public Task Delete(int id)
        {
            this.Delete_InputId = id;

            return Task.CompletedTask;
        }

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
    }
}