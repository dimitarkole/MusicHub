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

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
               .HasMany(e => e.Users)
               .WithOne(r => r.Role)
               .HasForeignKey(e => e.RoleId)
               .IsRequired();
        }
    }
}
