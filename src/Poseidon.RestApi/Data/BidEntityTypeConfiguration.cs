using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.RestApi.Bids;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// Configures the <see cref="BidEntity"/> for use within Entity Framework
    /// </summary>
    public class BidEntityTypeConfiguration : TradeEntityTypeConfigurationBase<BidEntity>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<BidEntity> builder)
        {
            builder.ToTable("BidList");

            builder.Property(b => b.Id)
                .HasColumnName("BidListId");

            base.Configure(builder);

            builder.Property(b => b.BidQuantity)
                .HasColumnName("bidQuantity");

            builder.Property(b => b.AskQuantity)
                .HasColumnName("askQuantity");

            builder.Property(b => b.Bid)
                .HasColumnName("bid");

            builder.Property(b => b.Ask)
                .HasColumnName("ask");

            builder.Property(b => b.BidListDate)
                .HasColumnName("bidListDate");

            builder.Property(b => b.Commentary)
                .HasColumnName("commentary")
                .HasMaxLength(125);
        }
    }
}