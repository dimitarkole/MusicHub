namespace MusicHub.Web.ViewModels.SongReactionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class SongReactionCreateInputModel : IMapTo<SongReaction>
    {
        public Reaction Reaction { get; set; }

        public string SongId { get; set; }
    }
}
