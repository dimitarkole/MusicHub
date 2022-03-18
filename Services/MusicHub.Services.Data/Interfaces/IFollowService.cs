using MusicHub.Web.ViewModels.FollowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Interfaces
{
    public interface IFollowService
    {
        FollowPageModel<T> AllFollowers<T>(int page, int entitesPerPage, string userId);

        FollowingPageModel<T> AllFollowing<T>(int page, int entitesPerPage, string userId);

        FollowPageModel<T> FilterFollowers<T>(int page, int entitesPerPage, FollowFilterInputModel filter, string userId);

        FollowingPageModel<T> FilterFollowings<T>(int page, int entitesPerPage, FollowFilterInputModel filter, string userId);

        Task Create(FollowCreateInputModel model, string userId);

        Task Delete(string id);

        bool IsFollowing(string followerUserId, string followingUserId);

        T GetFollow<T>(string followerUserId, string followingId);

        bool IsOwned(string id, string userId);
    }
}
