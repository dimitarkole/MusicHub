namespace MusicHub.Web.ViewModels.ReportMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class ReportMusicCreateInputModel : IMapTo<ReportMusic>
    {
        public ReportMusicType Type { get; set; }

        public string MusicId { get; set; }

        public string Description { get; set; }
    }
}
