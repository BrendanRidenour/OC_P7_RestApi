using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.CurvePoints
{
    /// <summary>
    /// An API controller for handling CRUD operations on the <see cref="CurvePointEntity" /> class
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class CurvePointController : EntityControllerBase<CurvePointEntity>
    {
        /// <summary>
        /// Instantiates the <see cref="CurvePointController" />
        /// </summary>
        /// <param name="crudStore">The CRUD store required for operation</param>
        public CurvePointController(ICrudStore<CurvePointEntity> crudStore)
            : base(crudStore)
        { }
    }
}