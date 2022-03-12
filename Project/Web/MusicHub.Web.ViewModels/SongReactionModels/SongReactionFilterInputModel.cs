namespace MusicHub.Web.ViewModels.SongReactionModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class SongReactionFilterInputModel : IMapTo<SongReaction>, IMapFrom<SongReaction>
    {
        public string SongId { get; set; }
    }
}
