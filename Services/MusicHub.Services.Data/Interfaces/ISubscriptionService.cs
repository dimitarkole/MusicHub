namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.SubscriptionModels;

    public interface ISubscriptionService
    {
        SubscriptionsAllViewModel<T> All<T>(int page, int entitesPerPage);

        SubscriptionsAllViewModel<T> Filter<T>(int page, int entitesPerPage, SubscriptionFilter filter);

        Task Create(SubscriptionCreateInputModel model);

        T GetById<T>(string id);

        Task Update(string id, SubscriptionEditInputModel model);

        Task Delete(string id);
    }
}
