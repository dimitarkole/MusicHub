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

    public class MusicLicenseConfiguration : IEntityTypeConfiguration<MusicLicense>
    {
        public void Configure(EntityTypeBuilder<MusicLicense> appMusicLicense)
        {
            appMusicLicense.HasOne(sl => sl.Music)
              .WithMany(s => s.MusicLicenses)
              .HasForeignKey(sl => sl.MusicId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
