using MusicHub.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class SubscriptionClaim : IAuditInfo
    {
        public SubscriptionClaim()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public string Description { get; set; }

        public string SubcriptionId { get; set; }

        public Subscription Subscription { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
