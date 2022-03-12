namespace MusicHub.Web.ViewModels.CommentModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class CommentReactionCreateModel : IMapTo<CommentReaction>
    {
        public Reaction Reaction { get; set; }

        public string CommentId { get; set; }
    }
}
