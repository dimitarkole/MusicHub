namespace MusicHub.Web.ViewModels.ReportMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.CategoryModels;
    using MusicHub.Web.ViewModels.MusicModels;

    public class ReportMusicViewModel : IMapFrom<Music>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public MusicUserViewModel User { get; set; }

        public string ImageUrl { get; set; }

        public string AudioUrl { get; set; }

        public virtual CategoryViewModel Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ReportMusicsCount { get; set; }
    }
}
