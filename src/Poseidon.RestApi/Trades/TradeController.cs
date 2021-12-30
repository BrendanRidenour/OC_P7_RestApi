using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Trades
{
    public class TradeController : EntityControllerBase<TradeEntity>
    {
        public TradeController(ICrudStore<TradeEntity> crudStore)
            : base(crudStore)
        { }
    }
}