using Microsoft.AspNetCore.Mvc;

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
        public virtual Task<T> Create([FromBody]T entity) => this.CrudStore.Create(entity);

        [HttpGet]
        [Route("{id}")]
        public virtual Task<T?> Read([FromRoute]int id) => this.CrudStore.Read(id);

        [HttpPut]
        [Route("{id}")]
        public virtual Task<T> Update([FromBody]T entity) => this.CrudStore.Update(entity);

        [HttpDelete]
        [Route("{id}")]
        public virtual Task Delete([FromRoute] int id) => this.CrudStore.Delete(id);
    }
}