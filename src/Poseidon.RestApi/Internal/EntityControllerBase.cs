using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;

namespace Poseidon.RestApi.Internal
{
    [ApiController]
    [Route("[controller]")]
    public abstract class EntityControllerBase<T> : ControllerBase
        where T : EntityBase, new()
    {
        protected ICrudStore<T> CrudStore { get; }

        protected EntityControllerBase(ICrudStore<T> crudStore)
        {
            this.CrudStore = crudStore ?? throw new ArgumentNullException(nameof(crudStore));
        }

        [HttpPost]
        public virtual async Task<ActionResult<T>> Create([FromBody]T entity) =>
            await this.CrudStore.Create(entity);

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<ActionResult<T?>> Read([FromRoute]int id)
        {
            var entity = await this.CrudStore.Read(id);

            if (entity is null)
                return NotFound();

            return entity;
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<ActionResult<T>> Update([FromRoute]int id,
            [FromBody]T entity)
        {
            if (id != entity.Id)
            {
                ModelState.AddModelError(nameof(entity.Id),
                    $"The id of the endpoint ('{id}') does not match the id of the entity to update ('{entity.Id}')");

                return BadRequest(ModelState);
            }

            return await this.CrudStore.Update(entity);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task Delete([FromRoute]int id) => this.CrudStore.Delete(id);
    }
}