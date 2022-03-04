namespace MusicHub.Web.ViewModels.AlbumMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.MusicModels;

    public class AlbumMusicViewModel : IMapFrom<AlbumMusic>
    {
        public virtual string Id { get; set; }

        public MusicViewModel Music { get; set; }
    }
}
