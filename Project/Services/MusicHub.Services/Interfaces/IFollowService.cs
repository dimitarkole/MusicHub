namespace MusicHub.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.FollowModels;

    public interface IFollowService
    {
        IEnumerable<T> AllFollowers<T>(string userId);

        IEnumerable<T> AllFollowed<T>(string userId);

        IEnumerable<T> Filter<T>(FollowFilterInputModel filter, string userId);

        Task Create(FollowCreateInputModel model, string userId);

        Task Delete(string id);

        bool IsFollowed(string followedUserId, string userId);

        T GetFollow<T>(string followedUserId, string userId);
    }
}
