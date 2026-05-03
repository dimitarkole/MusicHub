using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.UserLoginHistoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Interfaces
{
    public interface IUserLoginHistoryService
    {
        Task<string> Create(string userId, UserLoginHistoryCreateInputModel model);

        T GetById<T>(string id);

        Task Delete(string id);

        UserLoginHistoryPageModel<T> Filter<T>(int page, int entitesPerPage, string userId, UserLoginHistoryFilterInputModel filter);

        Task HandleLoginAsync(User user, string ipAddress, string userAgent);
    }
}
