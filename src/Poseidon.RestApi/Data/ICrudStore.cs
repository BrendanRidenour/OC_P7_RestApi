namespace Poseidon.RestApi.Data
{
    public interface ICrudStore<T> : IReadOperation<int, T>
        where T : Internal.EntityBase
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}