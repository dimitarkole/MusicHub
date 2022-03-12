namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlaylistViewModel : IMapFrom<Playlist>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int PlaylistSongsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public PlaylistUserViewModel User {get; set; }
    }
}
