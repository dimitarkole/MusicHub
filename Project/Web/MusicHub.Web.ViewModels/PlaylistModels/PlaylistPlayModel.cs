namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.SongModels;

    public class PlaylistPlayModel : IMapFrom<Playlist>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public PlaylistUserViewModel User { get; set; }
    }
}
