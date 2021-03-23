namespace MusicHub.Web.ViewModels.MusicReactionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class MusicReactionViewModel : IMapFrom<MusicReaction>, IMapTo<MusicReaction>
    {
        public Reaction Reaction { get; set; }

        public string Id { get; set; }
    }
}
