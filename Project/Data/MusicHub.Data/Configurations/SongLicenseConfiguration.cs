namespace MusicHub.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MusicHub.Data.Models;

    public class SongLicenseConfiguration : IEntityTypeConfiguration<SongLicense>
    {
        public void Configure(EntityTypeBuilder<SongLicense> appSongLicense)
        {
            appSongLicense.HasOne(sl => sl.Song)
              .WithMany(s => s.SongLicenses)
              .HasForeignKey(sl => sl.SongId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
