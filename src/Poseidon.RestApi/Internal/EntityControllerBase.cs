using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;

namespace Poseidon.RestApi.Internal
{
    [ApiController]
    [Route("[controller]")]
    public abstract class EntityControllerBase<T> : EntityControllerHelperBase<T>
        where T : EntityBase, new()
    {
        protected override string ReadEntityActionName => nameof(Read);

        protected EntityControllerBase(ICrudStore<T> crudStore)
            : base(crudStore)
        { }

        [HttpPost]
        public virtual Task<ActionResult<T>> Create([FromBody] T entity) =>
            base.CreateEntity(entity);

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ActionResult<T?>> Read([FromRoute] int id) =>
            base.ReadEntity(id);

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ActionResult> Update([FromRoute] int id, [FromBody] T entity) =>
            base.UpdateEntity(id, entity);

        [HttpDelete]
        [Route("{id}")]
        public virtual Task<ActionResult> Delete([FromRoute] int id) =>
            base.DeleteEntity(id);
    }
}