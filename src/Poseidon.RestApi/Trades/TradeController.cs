using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Trades
{
    [Authorize(Roles = "Admin")]
    public class TradeController : EntityControllerBase<TradeEntity>
    {
        public TradeController(ICrudStore<TradeEntity> crudStore)
            : base(crudStore)
        { }
    }
}