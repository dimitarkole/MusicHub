using MusicHub.Data.Common.Models;
using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Subscription : IAuditInfo
    {
        public Subscription()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Id = Guid.NewGuid().ToString();
            this.SubcriptionClaims = new HashSet<SubscriptionClaim>();
            this.PaySubscriptions = new HashSet<PaySubscription>();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public SubscriptionType Type { get; set; }

        public ICollection<SubscriptionClaim> SubcriptionClaims { get; set; }

        public ICollection<PaySubscription> PaySubscriptions { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
