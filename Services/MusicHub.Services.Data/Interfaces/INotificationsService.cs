using MusicHub.Web.ViewModels.NotificationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Interfaces
{
    public interface INotificationsService
    {
        Task Add(NotificationInputModel model);

        NotificationPageModel<T> GetUserNotifications<T>(int page, int entitesPerPage, string userId);

        Task MarkAsSeen(string id);

        string GetNotificationOwner(string notificationId);
    }
}
