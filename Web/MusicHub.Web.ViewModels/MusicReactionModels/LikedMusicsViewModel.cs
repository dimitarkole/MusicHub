namespace MusicHub.Web.ViewModels.MusicReactionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.MusicModels;

    public class LikedMusicsViewModel : IMapFrom<MusicReaction>
    {
        public virtual string Id { get; set; }

        public MusicViewModel Music { get; set; }
    }
}
