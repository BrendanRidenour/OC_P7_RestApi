using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// Base type for configuring entities for use within Entity Framework
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to configure</typeparam>
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
         where TEntity : Internal.EntityBase
    {
        /// <summary>
        /// Configures the entity for use within Entity Framework
        /// </summary>
        /// <param name="builder">The entity type builder to use for configuration</param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .UseIdentityColumn(seed: 100, increment: 1);
        }
    }
}
