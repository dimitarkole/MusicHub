using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Common.Models
{
    public class AuditableEntity<TKey> : Entity<TKey>, IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
