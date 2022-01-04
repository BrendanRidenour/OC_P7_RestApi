using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Ratings
{
    /// <summary>
    /// An API controller for handling CRUD operations on the <see cref="RatingEntity" /> class
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class RatingController : EntityControllerBase<RatingEntity>
    {
        /// <summary>
        /// Instantiates the <see cref="RatingController" />
        /// </summary>
        /// <param name="crudStore">The CRUD store required for operation</param>
        public RatingController(ICrudStore<RatingEntity> crudStore)
            : base(crudStore)
        { }
    }
}