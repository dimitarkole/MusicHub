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
        FollowersAllViewModel<T> AllFollowers<T>(int page, int entitesPerPage, string userId);

        FollowingsAllViewModel<T> AllFollowing<T>(int page, int entitesPerPage, string userId);

        FollowersAllViewModel<T> FilterFollowers<T>(int page, int entitesPerPage, FollowFilterInputModel filter, string userId);

        FollowingsAllViewModel<T> FilterFollowings<T>(int page, int entitesPerPage, FollowFilterInputModel filter, string userId);

        Task Create(FollowCreateInputModel model, string userId);

        Task Delete(string id);

        bool IsFollowing(string followerUserId, string followingUserId);

        T GetFollow<T>(string followerUserId, string followingId);

        bool IsOwned(string id, string userId);
    }
}
