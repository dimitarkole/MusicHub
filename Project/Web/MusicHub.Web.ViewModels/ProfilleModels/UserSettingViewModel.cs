namespace MusicHub.Web.ViewModels.ProfilleModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;

    public class UserSettingViewModel
    {
        public int Id { get; set; }

        public SettingType Type { get; set; }

        public bool Enabled { get; set; }
    }
}
