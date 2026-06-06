namespace MusicHub.Web.ViewModels.ReportMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Models.Enums;

    public class ReportFilter
    {
        public ReportMusicType Type { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
