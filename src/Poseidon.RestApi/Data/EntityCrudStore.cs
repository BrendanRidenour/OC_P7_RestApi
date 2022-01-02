using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Data
{
    public class EntityCrudStore<TEntity> : ICrudStore<TEntity>
        where TEntity : EntityBase, new()
    {
        protected PoseidonDbContext Db;

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