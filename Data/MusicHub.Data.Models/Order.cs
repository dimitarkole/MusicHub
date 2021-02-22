namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;

    public class Order : IAuditInfo
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual string SubscriptionId { get; set; }

        public virtual Subscription Subscription { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
