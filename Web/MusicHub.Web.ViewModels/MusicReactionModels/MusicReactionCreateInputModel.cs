namespace MusicHub.Web.ViewModels.MusicReactionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class MusicReactionCreateInputModel : IMapTo<MusicReaction>
    {
        public Reaction Reaction { get; set; }

        public string MusicId { get; set; }
    }
}
