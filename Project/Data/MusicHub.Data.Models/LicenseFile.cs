namespace MusicHub.Data.Models
{
    using System;

    using MusicHub.Data.Common.Models;

    public class LicenseFile : IAuditInfo
    {
        public LicenseFile()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public virtual string LicensеId { get; set; }

        public virtual License Licensе { get; set; }

        public string Path { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
