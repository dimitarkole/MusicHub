using MusicHub.Data.Common.Models;
using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class NotificationClaim : IAuditInfo
    {
        public NotificationClaim()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string NotificationId { get; set; }

        public virtual Notification Notification { get; set; }

        public NotificationClaimType Type { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
