using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Bids
{
    [Authorize]
    [Route("BidList")]
    public class BidController : EntityControllerBase<BidEntity>
    {
        public BidController(ICrudStore<BidEntity> crudStore) : base(crudStore) { }
    }
}