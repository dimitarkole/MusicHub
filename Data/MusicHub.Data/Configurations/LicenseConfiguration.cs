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

    public class LicenseConfiguration : IEntityTypeConfiguration<License>
    {
        public void Configure(EntityTypeBuilder<License> appLicense)
        {
            appLicense.HasMany(l => l.LicenseFiles)
                .WithOne(lf => lf.Licensе)
                .HasForeignKey(lf => lf.LicensеId)
                .OnDelete(DeleteBehavior.Restrict);

            appLicense.HasMany(l => l.MusicLicenses)
                .WithOne(sl => sl.License)
                .HasForeignKey(sl => sl.LicenseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
