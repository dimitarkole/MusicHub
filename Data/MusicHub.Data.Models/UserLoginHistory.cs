using MusicHub.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class UserLoginHistory : AuditableEntity<string>
    {
        public UserLoginHistory()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual User User { get; set; }

        public string UserId { get; set; }

        public string IpAddress { get; set; }

        public DateTime LoginDate { get; set; }

        public string? UserAgent { get; set; }
    }
}
