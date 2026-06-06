namespace MusicHub.Web.ViewModels.NotificationSetingModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class NotificationSettingsViewModel : IMapFrom<NotificationSetting>
    {
        public string Id { get; set; }

        public NotificationSettingType Type { get; set; }

        public bool Enabled { get; set; }
    }
}
