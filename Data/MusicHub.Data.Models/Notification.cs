namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;

    public class Notification : AuditableEntity<string>
    {
        public bool Seen { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
