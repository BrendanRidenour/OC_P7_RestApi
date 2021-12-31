namespace Poseidon.RestApi.Data
{
    public interface IReadOperation<TId, TData>
    {
        Task<TData?> Read(TId id);
    }
}