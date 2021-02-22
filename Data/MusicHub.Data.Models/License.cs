namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models.Enums;

    public class License : IAuditInfo
    {
        public License()
        {
            this.Id = Guid.NewGuid().ToString();
            this.MusicLicenses = new HashSet<MusicLicense>();
            this.LicenseFiles = new HashSet<LicenseFile>();
            this.CreatedOn = DateTime.UtcNow;
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public LicenseStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<MusicLicense> MusicLicenses { get; set; }

        public virtual ICollection<LicenseFile> LicenseFiles { get; set; }
    }
}
