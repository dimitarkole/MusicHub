namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common;
    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models.Enums;

    public class UserIpRule : Entity<string>, IAuditInfo
    {
        public UserIpRule()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public IpRuleType RuleType { get; set; }

        public string Pattern { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsEnabled { get; set; } = true;

        public string UserId { get; set; }

        public virtual User User { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
