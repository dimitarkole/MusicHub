namespace MusicHub.Web.ViewModels.CommentModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class CommentReactionEditInputModel : IMapTo<CommentReaction>
    {
        public Reaction Reaction { get; set; }
    }
}
