namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.NotificationModels;

    public class NotificationsService : INotificationsService
    {
        private readonly ApplicationDbContext context;

        public NotificationsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(NotificationInputModel model)
        {
            Notification notification = model.To<Notification>();
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<NotificationInputModel> models)
        {
            IEnumerable<Notification> notifications = models.Select(m => m.To<Notification>());
            await context.Notifications.AddRangeAsync(notifications);
            await context.SaveChangesAsync();
        }

        public IEnumerable<T> GetUserNotifications<T>(string userId) =>
            context.Notifications
            .Where(n => n.UserId == userId && !n.Seen)
            .To<T>()
            .ToList();

        public async Task MarkAsSeen(string id)
        {
            Notification notification = context.Notifications.Find(id);
            notification.Seen = true;
            context.Notifications.Update(notification);
            await context.SaveChangesAsync();
        }

        public string GetNotificationOwner(string notificationId) =>
            context.Notifications
            .Where(n => n.Id == notificationId)
            .Select(n => n.UserId)
            .FirstOrDefault();
    }
}
