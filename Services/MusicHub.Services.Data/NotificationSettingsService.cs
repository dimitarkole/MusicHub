namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.NotificationSetingModels;

    public class NotificationSettingsService : INotificationSettingsService
    {
        private readonly ApplicationDbContext context;

        public NotificationSettingsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> All<T>(string userId)
            => this.context.NotificationSettings
                .Where(ns => ns.UserId == userId)
                .To<T>()
                .ToList();

        public async Task<string> Create(NotificationSettingCreateInputModel model, string userId)
        {
            var notificationSetting = model.To<NotificationSetting>();
            notificationSetting.UserId = userId;
            await this.context.NotificationSettings.AddAsync(notificationSetting);
            await this.context.SaveChangesAsync();
            return notificationSetting.Id;
        }

        public async Task Delete(string id)
        {
            var notificationSetting = this.context.NotificationSettings.Find(id);
            this.context.NotificationSettings.Remove(notificationSetting);
            await this.context.SaveChangesAsync();
        }

        public T GetByType<T>(NotificationSettingType type, string userId)
            => this.context.NotificationSettings
                .Where(ns => ns.UserId == userId
                    && ns.Type == type)
                .To<T>()
                .FirstOrDefault();

        public bool IsEnabled(NotificationSettingType type, string userId)
            => !this.context.NotificationSettings
                .Any(ns => ns.UserId == userId
                    && ns.Type == type
                    && ns.Enabled == false);

        public bool IsOwned(string id, string userId)
            => this.context.NotificationSettings
                .Any(ns => ns.UserId == userId
                    && ns.Id == id);

        public async Task Update(string id, NotificationSettingEditInputModel model)
        {
            var notificationSetting = this.context.NotificationSettings.Find(id);
            model.To<NotificationSetting>(notificationSetting);
            this.context.NotificationSettings.Update(notificationSetting);
            await this.context.SaveChangesAsync();
        }
    }
}
