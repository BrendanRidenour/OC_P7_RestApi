namespace Poseidon.RestApi.Internal
{
    public interface IEntityBasePropertyCopy<TEntity> where TEntity : EntityBase
    {
        void CopyProperties(TEntity entity);
    }
}