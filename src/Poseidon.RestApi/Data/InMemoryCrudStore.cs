namespace Poseidon.RestApi.Data
{
    public class InMemoryCrudStore<T> : ICrudStore<T>
        where T : Internal.EntityBase
    {
        public static List<T> Store = new List<T>();

        public Task<T> Create(T entity)
        {
            var maxId = Store.Any() ? Store.Max(x => x.Id) : 0;

            entity.Id = maxId + 1;

            Store.Add(entity);

            return Task.FromResult(entity);
        }

        public Task<T?> Read(int id)
        {
            return Task.FromResult(Store.FirstOrDefault(x => x.Id == id));
        }

        public Task<T> Update(T entity)
        {
            var index = Store.FindIndex(x => x.Id == entity.Id);

            if (index == -1)
                throw new InvalidOperationException($"No entity exists with this id: {entity.Id}");

            Store[index] = entity;

            return Task.FromResult(entity);
        }

        public Task Delete(int id)
        {
            Store.RemoveAll(x => x.Id == id);

            return Task.CompletedTask;
        }
    }
}