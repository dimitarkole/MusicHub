namespace MusicHub.Web.ViewModels.ReportMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.Users;

    public class ReportViewModel : IMapFrom<ReportMusic>
    {
        public string Id { get; set; }

        public ReportMusicType Type { get; set; }

        public string MusicId { get; set; }

        public bool Seen { get; set; }

        public UserViewModel User { get; set; }
    }
}
