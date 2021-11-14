using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Detran.Infrastructure.Entity.Configurations
{
    public class ApiUserRoleModelConfigurations : IEntityTypeConfiguration<ApiUserRole>
    {
        public void Configure(EntityTypeBuilder<ApiUserRole> builder)
        {
            builder.ToTable("ApiUserRole");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Role).HasMaxLength(5).HasColumnType("VARCHAR").IsRequired();
            builder.Property(e => e.Description).HasMaxLength(50).HasColumnType("VARCHAR").IsRequired();

            builder.HasIndex(e => e.Role);
            builder.HasMany(e => e.Users).WithMany(e => e.Roles);
        }
    }
}
