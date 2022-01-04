using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Bids
{
    /// <summary>
    /// An API controller for handling CRUD operations on the <see cref="BidEntity" /> class
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("BidList")]
    public class BidController : EntityControllerBase<BidEntity>
    {
        /// <summary>
        /// Instantiates the <see cref="BidController" />
        /// </summary>
        /// <param name="crudStore">The CRUD store required for operation</param>
        public BidController(ICrudStore<BidEntity> crudStore) : base(crudStore) { }
    }
}