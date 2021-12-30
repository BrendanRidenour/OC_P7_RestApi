using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.CurvePoints
{
    public class CurvePointController : EntityControllerBase<CurvePointEntity>
    {
        public CurvePointController(ICrudStore<CurvePointEntity> crudStore)
            : base(crudStore)
        { }
    }
}