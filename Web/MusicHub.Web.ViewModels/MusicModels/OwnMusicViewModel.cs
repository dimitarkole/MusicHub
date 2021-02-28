namespace MusicHub.Web.ViewModels.MusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class OwnMusicViewModel : IMapFrom<Music>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Path { get; set; }

        public string MusicCategoryId { get; set; }

        public string MusicCategoryName { get; set; }

        public string Text { get; set; }

        // public int Rating { get; set; }

        public long CountLikes { get; set; }

        public long CountViews { get; set; }

        public long CountDisLikes { get; set; }
    }
}