using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Trades
{
    /// <summary>
    /// An API controller for handling CRUD operations on the <see cref="TradeEntity" /> class
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class TradeController : EntityControllerBase<TradeEntity>
    {
        /// <summary>
        /// Instantiates the <see cref="TradeController" />
        /// </summary>
        /// <param name="crudStore">The CRUD store required for operation</param>
        public TradeController(ICrudStore<TradeEntity> crudStore)
            : base(crudStore)
        { }
    }
}