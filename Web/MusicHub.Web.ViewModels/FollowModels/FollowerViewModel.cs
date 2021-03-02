namespace MusicHub.Web.ViewModels.FollowModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class FollowerViewModel : IMapFrom<Follow>
    {
        public string Id { get; set; }

        public FollowUserViewModel Follower { get; set; }
    }
}
