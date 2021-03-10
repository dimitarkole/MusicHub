namespace MusicHub.Web.ViewModels.PlayedMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.MusicModels;

    public class PlayedMusicViewModel : IMapFrom<PlayedMusic>
    {
        public string Id { get; set; }

        public virtual MusicViewModel Music { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
