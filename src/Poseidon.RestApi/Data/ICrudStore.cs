namespace Poseidon.RestApi.Data
{
    public interface ICrudStore<T> : IReadOperation<int, T>
        where T : Internal.EntityBase
    {
        Task<T> Create(T entity);
        Task<IEnumerable<T>> Read();
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}