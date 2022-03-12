namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Common.Models;

    public class License: IAuditInfo
    {
        public License()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SongLicenses = new HashSet<SongLicense>();
            this.LicenseFiles = new HashSet<LicenseFile>();
            this.CreatedOn = DateTime.UtcNow;
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public LicenseStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<SongLicense> SongLicenses { get; set; }

        public virtual ICollection<LicenseFile> LicenseFiles { get; set; }
    }
}
