namespace Poseidon.RestApi.Internal
{
    public interface IEntityBasePropertyCopy<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Copies the properties (excluding the Id) from the given <paramref name="entity" />
        /// argument and onto this instance
        /// </summary>
        /// <param name="entity">The argument whose properties to copy</param>
        void CopyProperties(TEntity entity);
    }
}