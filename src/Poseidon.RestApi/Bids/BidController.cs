using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Bids
{
    [Route("BidList")]
    public class BidController : EntityControllerBase<BidEntity>
    {
        public BidController(ICrudStore<BidEntity> crudStore) : base(crudStore) { }
    }
}