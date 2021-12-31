using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Data
{
    public class InMemoryCrudStore<T>
        : ICrudStore<T>, IReadOperation<Username, UserEntity>
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

        public Task<UserEntity?> Read(Username username)
        {
            var user = username.Value == "Bwe"
                ? new UserEntity()
                {
                    Id = 979,
                    Username = username.Value,
                    Password = "25000.Vx3WU+ftjLFoP7fVP3vnD/eA7d2Fmxv8/9vJPk4hWtUJeczS7kdDhkd4te022U55",
                    Fullname = "Bwe Ride",
                    Role = "Admin",
                }
                : null;

            return Task.FromResult(user);
        }
    }
}