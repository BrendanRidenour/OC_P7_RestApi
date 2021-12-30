namespace Poseidon.RestApi.Data
{
    public interface ICrudStore<T> where T : Internal.EntityBase
    {
        Task<T> Create(T entity);
        Task<T?> Read(int id);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}