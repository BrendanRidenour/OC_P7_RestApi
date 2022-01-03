using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.CurvePoints
{
    [Authorize(Roles = "Admin")]
    public class CurvePointController : EntityControllerBase<CurvePointEntity>
    {
        public CurvePointController(ICrudStore<CurvePointEntity> crudStore)
            : base(crudStore)
        { }
    }
}