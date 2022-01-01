using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;

namespace Poseidon.RestApi.Internal
{
    public abstract class EntityControllerHelperBase<T> : ControllerBase
        where T : EntityBase, new()
    {
        protected ICrudStore<T> CrudStore { get; }
        protected abstract string ReadEntityActionName { get; }

        protected EntityControllerHelperBase(ICrudStore<T> crudStore)
        {
            this.CrudStore = crudStore ?? throw new ArgumentNullException(nameof(crudStore));
        }

        protected virtual async Task<ActionResult<T>> CreateEntity(T entity)
        {
            entity = await this.CrudStore.Create(entity);
            
            return CreatedAtAction(ReadEntityActionName, new { id = entity.Id }, entity);
        }

        protected virtual async Task<ActionResult<T?>> ReadEntity(int id)
        {
            var entity = await this.CrudStore.Read(id);

            if (entity is null)
                return NotFound();

            return entity;
        }

        protected virtual async Task<ActionResult> UpdateEntity(int id, T entity)
        {
            var existingEntity = await this.CrudStore.Read(id);
            if (existingEntity is null)
                return NotFound();

            entity.Id = id;

            await this.CrudStore.Update(entity);

            return NoContent();
        }

        protected virtual async Task<ActionResult> DeleteEntity(int id)
        {
            var entity = await this.CrudStore.Read(id);

            if (entity is null)
                return NotFound();

            await this.CrudStore.Delete(id);

            return NoContent();
        }
    }
}