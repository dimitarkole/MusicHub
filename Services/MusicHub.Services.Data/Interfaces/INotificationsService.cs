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

        Task AddRange(IEnumerable<NotificationInputModel> models);

        IEnumerable<T> GetUserNotifications<T>(string userId);

        Task MarkAsSeen(string id);

        string GetNotificationOwner(string notificationId);
    }
}
