namespace MusicHub.Web.ViewModels.SongReactionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.SongModels;

    public class LikedSongsViewModel : IMapFrom<SongReaction>
    {
        public virtual string Id { get; set; }

        public SongViewModel Song { get; set; }
    }
}
