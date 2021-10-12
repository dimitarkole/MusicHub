using MusicHub.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class PaySubscription : IAuditInfo
    {
        public PaySubscription()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string SubscriptionId { get; set; }

        public virtual Subscription Subscription { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
