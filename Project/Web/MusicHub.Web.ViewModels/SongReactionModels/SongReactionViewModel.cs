namespace MusicHub.Web.ViewModels.SongReactionModels
{
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class SongReactionViewModel : IMapFrom<SongReaction>, IMapTo<SongReaction>
    {
        public Reaction Reaction { get; set; }

        public string Id { get; set; }
    }
}
