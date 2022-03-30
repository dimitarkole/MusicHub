namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models.Enums;
    using MusicHub.Web.ViewModels.NotificationSetingModels;

    public interface INotificationSettingsService
    {
        IEnumerable<T> All<T>(string userId);

        T GetByType<T>(NotificationSettingType type, string userId);

        Task<string> Create(NotificationSettingCreateInputModel model, string userId);

        Task Update(string id, NotificationSettingEditInputModel model);

        Task Delete(string id);

        bool IsEnabled(NotificationSettingType type, string userId);

        bool IsOwned(string id, string userId);
    }
}
