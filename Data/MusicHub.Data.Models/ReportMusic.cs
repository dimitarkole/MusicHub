namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models.Enums;

    public class ReportMusic : AuditableEntity<string>, IAuditInfo
    {
        public ReportMusic()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ReportMusicType Type { get; set; }

        public string MusicId { get; set; }

        public Music Music { get; set; }

        public string Description { get; set; }

        public bool Seen { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
