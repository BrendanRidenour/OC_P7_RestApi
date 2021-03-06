using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.RestApi.Ratings;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// Configures the <see cref="RatingEntity"/> for use within Entity Framework
    /// </summary>
    public class RatingEntityTypeConfiguration : EntityTypeConfigurationBase<RatingEntity>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<RatingEntity> builder)
        {
            builder.ToTable("Rating");

            base.Configure(builder);

            builder.Property(b => b.MoodysRating)
                .HasColumnName("moodysRating")
                .HasMaxLength(125);

            builder.Property(b => b.SandPRating)
                .HasColumnName("sandPRating")
                .HasMaxLength(125);

            builder.Property(b => b.FitchRating)
                .HasColumnName("fitchRating")
                .HasMaxLength(125);

            builder.Property(b => b.OrderNumber)
                .HasColumnName("orderNumber");
        }
    }
}