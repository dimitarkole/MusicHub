namespace MusicHub.Data.Models
{
    using System;

    using MusicHub.Data.Common.Models;

    public class SongLicense : IAuditInfo
    {
        public SongLicense()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public virtual string SongId { get; set; }

        public virtual Song Song { get; set; }

        public virtual string LicenseId { get; set; }

        public virtual License License { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
