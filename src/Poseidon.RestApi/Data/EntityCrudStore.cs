using Microsoft.EntityFrameworkCore;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// A class to perform CRUD operations against an entity
    /// </summary>
    /// <typeparam name="TEntity">The entity type on which to perform CRUD operations</typeparam>
    public class EntityCrudStore<TEntity> : ICrudStore<TEntity>
        where TEntity : EntityBase, new()
    {
        protected PoseidonDbContext Db;

        /// <summary>
        /// Instantiates the class
        /// </summary>
        /// <param name="db">The Entity Framework data context to use to perform CRUD operations</param>
        public EntityCrudStore(PoseidonDbContext db)
        {
            this.Db = db;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            this.Db.Add(entity);
            await this.Db.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> Read() =>
            await this.Db.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> Read(int id) =>
            await this.Db.FindAsync<TEntity>(id);

        public async Task<TEntity> Update(TEntity entity)
        {
            this.Db.Update(entity);

            await this.Db.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await this.Db.FindAsync<TEntity>(id);

            if (entity is null)
                return;

            this.Db.Remove(entity);
            
            await this.Db.SaveChangesAsync();
        }
    }
}