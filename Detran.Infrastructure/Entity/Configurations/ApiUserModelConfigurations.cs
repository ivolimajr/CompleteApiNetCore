using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Detran.Infrastructure.Entity.Configurations
{
    public class ApiUserModelConfigurations : IEntityTypeConfiguration<ApiUserModel>
    {
        public void Configure(EntityTypeBuilder<ApiUserModel> builder)
        {
            builder.ToTable("ApiUser");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserName).HasMaxLength(150).HasColumnType("varchar").IsRequired();
            builder.Property(e => e.Name).HasMaxLength(70).HasColumnType("varchar").IsRequired();
            builder.Property(e => e.Password).HasMaxLength(150).HasColumnType("varchar").IsRequired();
            builder.Property(e => e.RefreshToken).HasMaxLength(250).HasColumnType("varchar");
            builder.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValue(DateTime.Now).IsRequired();
            builder.Property(e => e.UpdateddAt).HasColumnType("datetime");
            builder.Property(e => e.DeletedAt).HasColumnType("datetime");

            builder.HasMany(e => e.Roles).WithMany(e => e.Users);
        }
    }
}