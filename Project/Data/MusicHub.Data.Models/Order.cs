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

        //
        // Summary:
        //     Gets or sets the primary key for this role.
        public virtual string Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual string PlanId { get; set; }

        public virtual Plan Plan { get; set; }

        public virtual string VaucherId { get; set; }

        public virtual Vaucher? Vaucher { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
