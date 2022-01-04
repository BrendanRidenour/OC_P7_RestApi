using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// Configures the <see cref="UserEntity"/> for use within Entity Framework
    /// </summary>
    public class UserEntityTypeConfiguration : EntityTypeConfigurationBase<UserEntity>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");

            base.Configure(builder);

            builder.Property(e => e.Username)
                .HasColumnName("username")
                .HasMaxLength(125);

            builder.HasIndex(e => e.Username)
                .IsUnique();

            builder.Property(e => e.Password)
                .HasColumnName("password")
                .HasMaxLength(125);

            builder.Property(e => e.Fullname)
                .HasColumnName("fullname")
                .HasMaxLength(125);

            builder.Property(e => e.Role)
                .HasColumnName("role")
                .HasMaxLength(125);
        }
    }
}