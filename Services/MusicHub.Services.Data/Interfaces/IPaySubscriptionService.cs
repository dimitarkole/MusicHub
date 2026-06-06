using MusicHub.Web.ViewModels.PaySubscriptionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Interfaces
{
    public interface IPaySubscriptionService
    {

        PaySubscriptionPageModel<T> All<T>(int page, int entitesPerPage, string userId = null);

        PaySubscriptionPageModel<T> Filter<T>(int page, int entitesPerPage, PaySubscriptionFilter filter, string userId = null);

        Task<string> Create(PaySubscriptionCreateInputModel model, string userId);

        T GetById<T>(string id);

        bool IsOwned(string id, string userId);

        Task Update(string id, PaySubscriptionEditModel model);
    }
}
