namespace MusicHub.Web.ViewModels.MusicReactionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class MusicReactionFilterInputModel : IMapTo<MusicReaction>, IMapFrom<MusicReaction>
    {
        public string MusicId { get; set; }
    }
}
