using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.CommentModels
{
    public class CommentReactionEditInputModel : IMapTo<CommentReaction>
    {
        public Reaction Reaction { get; set; }

        public string CommentId { get; set; }
    }
}
