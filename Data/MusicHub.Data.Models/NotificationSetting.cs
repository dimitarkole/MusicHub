namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models.Enums;

    public class NotificationSetting : AuditableEntity<string>, IAuditInfo
    {
        public NotificationSetting()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public NotificationSettingType Type { get; set; }

        public bool Enabled { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
