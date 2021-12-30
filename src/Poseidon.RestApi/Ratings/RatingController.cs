using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Ratings
{
    public class RatingController : EntityControllerBase<RatingEntity>
    {
        public RatingController(ICrudStore<RatingEntity> crudStore)
            : base(crudStore)
        { }
    }
}