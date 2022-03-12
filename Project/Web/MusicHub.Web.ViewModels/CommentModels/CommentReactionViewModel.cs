namespace MusicHub.Web.ViewModels.CommentModels
{
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class CommentReactionViewModel : IMapFrom<CommentReaction>
    {
        public Reaction Reaction { get; set; }

        public string Id { get; set; }
    }
}
