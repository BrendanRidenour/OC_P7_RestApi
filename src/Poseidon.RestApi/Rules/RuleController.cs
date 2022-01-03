using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Rules
{
    [Authorize(Roles = "Admin")]
    public class RuleController : EntityControllerBase<RuleEntity>
    {
        public RuleController(ICrudStore<RuleEntity> crudStore) : base(crudStore) { }
    }
}