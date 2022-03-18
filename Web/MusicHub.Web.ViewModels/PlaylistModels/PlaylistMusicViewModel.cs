namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.MusicModels;

    public class PlaylistMusicViewModel : IMapFrom<PlaylistMusic>
    {
        public string Id { get; set; }

        public MusicViewModel Music { get; set; }
    }
}
