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
    public class ReportMusicConfiguration : IEntityTypeConfiguration<ReportMusic>
    {
        public void Configure(EntityTypeBuilder<ReportMusic> appReportMusic)
        {
            appReportMusic
              .HasOne(e => e.User)
              .WithMany(e => e.ReportMusics)
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            appReportMusic
             .HasOne(e => e.Music)
             .WithMany(e => e.ReportMusics)
             .HasForeignKey(e => e.MusicId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
