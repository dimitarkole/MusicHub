namespace MusicHub.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MusicHub.Data.Models;

    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(e => new { e.RoleId, e.PermissionId });

            builder.HasOne(e => e.Permission)
                .WithMany(p => p.Roles)
                .HasForeignKey(e => e.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
