using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.RestApi.Trades;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// Configures the <see cref="TradeEntity"/> for use within Entity Framework
    /// </summary>
    public class TradeEntityTypeConfiguration : TradeEntityTypeConfigurationBase<TradeEntity>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<TradeEntity> builder)
        {
            builder.ToTable("Trade");

            builder.Property(b => b.Id)
                .HasColumnName("TradeId");

            base.Configure(builder);

            builder.Property(b => b.BuyQuantity)
                .HasColumnName("buyQuantity");

            builder.Property(b => b.SellQuantity)
                .HasColumnName("sellQuantity");

            builder.Property(b => b.BuyPrice)
                .HasColumnName("buyPrice");

            builder.Property(b => b.SellPrice)
                .HasColumnName("sellPrice");
        }
    }
}