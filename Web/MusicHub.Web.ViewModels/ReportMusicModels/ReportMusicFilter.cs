namespace MusicHub.Web.ViewModels.ReportMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Models.Enums;

    public class ReportMusicFilter
    {
        public string Name { get; set; }

        public string CategoryId { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
