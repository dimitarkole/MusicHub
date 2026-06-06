namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Common;
    using MusicHub.Data.Common;
    using MusicHub.Data.Common.Models;

    public class VerificationCode : Entity<string>, IAuditInfo
    {
        public VerificationCode()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsUsed = false;
        }

        public bool IsUsed { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string Code { get; set; }

        public VerificationType Type { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
