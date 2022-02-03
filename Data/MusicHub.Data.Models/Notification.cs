namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models.Enums;

    public class Notification : AuditableEntity<string>
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Claims = new HashSet<NotificationClaim>();
        }

        public bool Seen { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }

        public string UserId { get; set; }

        public ICollection<NotificationClaim> Claims { get; set; }

        public User User { get; set; }
    }
}
