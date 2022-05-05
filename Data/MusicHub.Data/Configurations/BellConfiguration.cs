using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Configurations
{
    public class BellConfiguration : IEntityTypeConfiguration<Bell>
    {
        public void Configure(EntityTypeBuilder<Bell> appBeel)
        {
            appBeel.HasKey(e => new { e.BellingId, e.BellerId });

            appBeel.HasOne(e => e.Belling)
                .WithMany(u => u.Bellers)
                .HasForeignKey(e => e.BellingId)
                .OnDelete(DeleteBehavior.Restrict);

            appBeel.HasOne(e => e.Beller)
                .WithMany(u => u.Bellings)
                .HasForeignKey(e => e.BellerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
