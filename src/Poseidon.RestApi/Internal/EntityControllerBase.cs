using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;

namespace Poseidon.RestApi.Internal
{
    [ApiController]
    [Route("[controller]")]
    public abstract class EntityControllerBase<TEntity> : EntityControllerHelperBase<TEntity>
        where TEntity : EntityBase, IEntityBasePropertyCopy<TEntity>, new()
    {
        protected override string ReadEntityActionName => nameof(Read);

        protected EntityControllerBase(ICrudStore<TEntity> crudStore)
            : base(crudStore)
        { }

        [HttpPost]
        public virtual Task<ActionResult<TEntity>> Create([FromBody] TEntity entity) =>
            base.CreateEntity(entity);

        [HttpGet]
        [Route("")]
        public virtual Task<IEnumerable<TEntity>> Read() =>
            base.ReadEntities();

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ActionResult<TEntity?>> Read([FromRoute] int id) =>
            base.ReadEntity(id);

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ActionResult> Update([FromRoute] int id, [FromBody] TEntity entity) =>
            base.UpdateEntity(id, entity);

        [HttpDelete]
        [Route("{id}")]
        public virtual Task<ActionResult> Delete([FromRoute] int id) =>
            base.DeleteEntity(id);
    }
}