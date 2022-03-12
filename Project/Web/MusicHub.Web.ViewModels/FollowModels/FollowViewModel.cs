namespace MusicHub.Web.ViewModels.FollowModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class FollowViewModel : IMapFrom<Follower>
    {
        public string Id { get; set; }

        public FollowUserViewModel Followed { get; set; }
    }
}
