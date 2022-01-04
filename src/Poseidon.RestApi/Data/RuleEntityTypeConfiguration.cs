using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.RestApi.Rules;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// Configures the <see cref="RuleEntity"/> for use within Entity Framework
    /// </summary>
    public class RuleEntityTypeConfiguration : EntityTypeConfigurationBase<RuleEntity>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<RuleEntity> builder)
        {
            builder.ToTable("RuleName");

            base.Configure(builder);

            builder.Property(b => b.Name)
                .HasColumnName("name")
                .HasMaxLength(125);

            builder.Property(b => b.Description)
                .HasColumnName("description")
                .HasMaxLength(125);

            builder.Property(b => b.Json)
                .HasColumnName("json")
                .HasMaxLength(125);

            builder.Property(b => b.Template)
                .HasColumnName("template")
                .HasMaxLength(512);

            builder.Property(b => b.SqlStr)
                .HasColumnName("sqlStr")
                .HasMaxLength(125);

            builder.Property(b => b.SqlPart)
                .HasColumnName("sqlPart")
                .HasMaxLength(125);
        }
    }
}