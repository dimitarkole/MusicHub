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
        IEnumerable<T> AllFollowers<T>(string userId);

        IEnumerable<T> AllFollowed<T>(string userId);

        IEnumerable<T> Filter<T>(FollowFilterInputModel filter, string userId);

        Task Create(FollowCreateInputModel model, string userId);

        Task Delete(string id);

        bool IsFollowed(string followedUserId, string userId);

        T GetFollow<T>(string followedUserId, string userId);
    }
}
