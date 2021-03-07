namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;

    public class MusicLicense : IAuditInfo
    {
        public MusicLicense()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public virtual string MusicId { get; set; }

        public virtual Music Music { get; set; }

        public virtual string LicenseId { get; set; }

        public virtual License License { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
