namespace MusicHub.Web.ViewModels.CommentModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class CommentReactionCreateModel : IMapTo<CommentReaction>
    {
        [Required]
        public Reaction Reaction { get; set; }

        [Required]
        public string CommentId { get; set; }
    }
}
