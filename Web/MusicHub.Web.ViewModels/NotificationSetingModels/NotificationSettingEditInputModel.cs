namespace MusicHub.Web.ViewModels.NotificationSetingModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class NotificationSettingEditInputModel : IMapTo<NotificationSetting>
    {
        public bool Enabled { get; set; }
    }
}