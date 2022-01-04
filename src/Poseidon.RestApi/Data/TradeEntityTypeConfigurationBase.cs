using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Poseidon.RestApi.Data
{
    /// <inheritdoc/>
    public abstract class TradeEntityTypeConfigurationBase<TEntity>
        : EntityTypeConfigurationBase<TEntity>
         where TEntity : Internal.TradeEntityBase
    {
        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.Account)
                .HasColumnName("account")
                .HasMaxLength(30);

            builder.Property(b => b.Type)
                .HasColumnName("type")
                .HasMaxLength(30);

            builder.Property(b => b.Benchmark)
                .HasColumnName("benchmark")
                .HasMaxLength(125);

            builder.Property(b => b.Security)
                .HasColumnName("security")
                .HasMaxLength(125);

            builder.Property(b => b.Status)
                .HasColumnName("status")
                .HasMaxLength(10);

            builder.Property(b => b.Trader)
                .HasColumnName("trader")
                .HasMaxLength(125);

            builder.Property(b => b.Book)
                .HasColumnName("book")
                .HasMaxLength(125);

            builder.Property(b => b.CreationName)
                .HasColumnName("creationName")
                .HasMaxLength(125);

            builder.Property(b => b.CreationDate)
                .HasColumnName("creationDate");

            builder.Property(b => b.RevisionName)
                .HasColumnName("revisionName")
                .HasMaxLength(125);

            builder.Property(b => b.RevisionDate)
                .HasColumnName("revisionDate");

            builder.Property(b => b.DealName)
                .HasColumnName("dealName")
                .HasMaxLength(125);

            builder.Property(b => b.DealType)
                .HasColumnName("dealType")
                .HasMaxLength(125);

            builder.Property(b => b.SourceListId)
                .HasColumnName("sourceListId")
                .HasMaxLength(125);

            builder.Property(b => b.Side)
                .HasColumnName("side")
                .HasMaxLength(125);
        }
    }
}