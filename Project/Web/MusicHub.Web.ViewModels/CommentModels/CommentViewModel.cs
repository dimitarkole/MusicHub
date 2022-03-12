namespace MusicHub.Web.ViewModels.CommentModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public CommentUserViewModel User { get; set; }

        public virtual ICollection<CommentViewModel> CommentsChildren { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
               .ForMember(
                   m => m.LikesCount,
                   y => y.MapFrom(
                       s => s.CommentReactions.Where(
                           r => r.Reaction == Reaction.Like)
                       .Count() == 0 ? 0 : s.CommentReactions.Where(
                           r => r.Reaction == Reaction.Like)
                       .Count()))
              .ForMember(
                 m => m.DislikesCount,
                 y => y.MapFrom(
                     s => s.CommentReactions.Where(
                         r => r.Reaction == Reaction.Dislike)
                     .Count() == 0 ? 0 : s.CommentReactions.Where(
                         r => r.Reaction == Reaction.Dislike)
                     .Count()));
        }
    }
}
