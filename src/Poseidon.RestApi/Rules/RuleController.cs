using Microsoft.AspNetCore.Authorization;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Rules
{
    /// <summary>
    /// An API controller for handling CRUD operations on the <see cref="RuleEntity" /> class
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class RuleController : EntityControllerBase<RuleEntity>
    {
        /// <summary>
        /// Instantiates the <see cref="RuleController" />
        /// </summary>
        /// <param name="crudStore">The CRUD store required for operation</param>
        public RuleController(ICrudStore<RuleEntity> crudStore) : base(crudStore) { }
    }
}