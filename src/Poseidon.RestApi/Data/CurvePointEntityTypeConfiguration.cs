using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.RestApi.CurvePoints;

namespace Poseidon.RestApi.Data
{
    public class CurvePointEntityTypeConfiguration : EntityTypeConfigurationBase<CurvePointEntity>
    {
        public override void Configure(EntityTypeBuilder<CurvePointEntity> builder)
        {
            builder.ToTable("CurvePoint");

            base.Configure(builder);

            builder.Property(b => b.AsOfDate)
                .HasColumnName("asOfDate");

            builder.Property(b => b.Term)
                .HasColumnName("term");

            builder.Property(b => b.Value)
                .HasColumnName("value");

            builder.Property(b => b.CreationDate)
                .HasColumnName("creationDate");
        }
    }
}