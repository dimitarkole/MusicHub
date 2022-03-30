namespace MusicHub.Web.ViewModels.NotificationSetingModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class NotificationSettingCreateInputModel : IMapTo<NotificationSetting>
    {
        [Required]
        public NotificationSettingType Type { get; set; }

        [Required]
        public bool Enabled { get; set; }
    }
}
