using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;

namespace Poseidon.RestApi.Internal
{
    /// <summary>
    /// A base controller with default implementations for all API CRUD operations
    /// </summary>
    /// <typeparam name="T">The entity on which to perform CRUD operations</typeparam>
    [ApiController]
    [Route("[controller]")]
    public abstract class EntityControllerBase<TEntity> : EntityControllerHelperBase<TEntity>
        where TEntity : EntityBase, IEntityBasePropertyCopy<TEntity>, new()
    {
        protected override string ReadEntityActionName => nameof(Read);

        protected EntityControllerBase(ICrudStore<TEntity> crudStore)
            : base(crudStore)
        { }

        /// <summary>
        /// Creates an entity and saves it to the backend data store
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <returns>A <see cref="CreatedAtActionResult"/> with information about the created entity</returns>
        [HttpPost]
        public virtual Task<ActionResult<TEntity>> Create([FromBody] TEntity entity) =>
            base.CreateEntity(entity);

        /// <summary>
        /// Gets all entities from the backend data store
        /// </summary>
        /// <returns>A list of entities</returns>
        [HttpGet]
        [Route("")]
        public virtual Task<IEnumerable<TEntity>> Read() =>
            base.ReadEntities();

        /// <summary>
        /// Gets an entity for the given <paramref name="id"/> if one exists
        /// </summary>
        /// <param name="id">The id to look for in the data store</param>
        /// <returns>The entity if found or a <see cref="NotFoundResult"/></returns>
        [HttpGet]
        [Route("{id}")]
        public virtual Task<ActionResult<TEntity?>> Read([FromRoute] int id) =>
            base.ReadEntity(id);

        /// <summary>
        /// Updates an entity in the backend store
        /// </summary>
        /// <param name="id">The <paramref name="id"/> of the entity to update</param>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="NoContentResult"/> after successful update or a <see cref="NotFoundResult"/> if no entity exists with that <paramref name="id"/></returns>
        [HttpPut]
        [Route("{id}")]
        public virtual Task<ActionResult> Update([FromRoute] int id, [FromBody] TEntity entity) =>
            base.UpdateEntity(id, entity);

        /// <summary>
        /// Delete an entity with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the entity to delete</param>
        /// <returns>A <see cref="NoContentResult"/> after successful deletion or a <see cref="NotFoundResult"/> if no entity exists with that <paramref name="id"/></returns>
        [HttpDelete]
        [Route("{id}")]
        public virtual Task<ActionResult> Delete([FromRoute] int id) =>
            base.DeleteEntity(id);
    }
}