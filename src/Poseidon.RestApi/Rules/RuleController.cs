using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Rules
{
    public class RuleController : EntityControllerBase<RuleEntity>
    {
        public RuleController(ICrudStore<RuleEntity> crudStore) : base(crudStore) { }
    }
}