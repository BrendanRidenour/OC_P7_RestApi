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
        public virtual async Task<ActionResult<T>> Create([FromBody]T entity)
        {
            entity = await this.CrudStore.Create(entity);

            return CreatedAtAction(nameof(Read), entity);
        }

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
        public virtual async Task<ActionResult> Update([FromRoute]int id,
            [FromBody]T entity)
        {
            if (id != entity.Id)
            {
                ModelState.AddModelError(nameof(entity.Id),
                    $"The id of the endpoint ('{id}') does not match the id of the entity to update ('{entity.Id}')");

                return BadRequest(ModelState);
            }

            var existingEntity = await this.CrudStore.Read(id);
            if (existingEntity is null)
                return NotFound();

            await this.CrudStore.Update(entity);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<ActionResult> Delete([FromRoute]int id)
        {
            var entity = await this.CrudStore.Read(id);

            if (entity is null)
                return NotFound();

            await this.CrudStore.Delete(id);

            return NoContent();
        }
    }
}