namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class PlaylistViewModel : IMapFrom<Playlist>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int PlaylistMusicsCount { get; set; }

        public string ImageUrl { get; set; }

        public PlaylistUserViewModel User { get; set; }

        public DateTime CreatedOn { get; set; }

        public VisibleStatus VisibleStatus { get; set; }
    }
}
