namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common;
    using MusicHub.Data.Common.Models;

    public class Bell : Entity<string>, IAuditInfo
    {
        public Bell()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string BeelerId { get; set; } // User Id

        public virtual User Beeler { get; set; }

        public string BellingId { get; set; } // Belling user Id

        public virtual User Belling { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
