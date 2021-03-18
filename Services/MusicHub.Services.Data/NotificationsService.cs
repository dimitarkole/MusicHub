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
    using MusicHub.Common.Extensions;

    public class NotificationsService : INotificationsService
    {
        private readonly ApplicationDbContext context;
        private readonly PaginationService paginationService;

        public NotificationsService(ApplicationDbContext context,
            PaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
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

        public NotificationAllViewModel<T> GetUserNotifications<T>(int page, int entitesPerPage, string userId)
        {
            var notifications = this.context.Notifications
                .Where(n => n.UserId == userId && !n.Seen)
                .OrderBy(n => n.Seen == false)
                .ThenByDescending(n => n.CreatedOn)
                .To<T>();

            return new NotificationAllViewModel<T>()
            {
                Notifications = notifications
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetNotificationsPagesCount(notifications.Count(), entitesPerPage),
            };
        }

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

        private int GetNotificationsPagesCount(int entityCount, int entitesPerPage)
             => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
