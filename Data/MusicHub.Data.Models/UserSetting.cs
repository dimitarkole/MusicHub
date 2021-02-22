using MusicHub.Data.Common;
using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class UserSetting : Entity<int>
    {
        public SettingType Type { get; set; }

        public bool Enabled { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
