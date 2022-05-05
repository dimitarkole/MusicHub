namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.BellModels;

    public interface IBellService
    {
        BellerPageModel<T> AllBellers<T>(int page, int entitesPerPage, string userId);

        BellingPageModel<T> AllBellings<T>(int page, int entitesPerPage, string userId);

        BellerPageModel<T> FilterBellers<T>(int page, int entitesPerPage, BellFilterInputModel filter, string userId);

        BellingPageModel<T> FilterBellings<T>(int page, int entitesPerPage, BellFilterInputModel filter, string userId);

        Task Create(BellCreateInputModel model, string userId);

        Task Delete(string id);

        bool IsBelling(string bellerUserId, string bellingUserId);

        T GetBell<T>(string bellerUserId, string bellingUserId);

        bool IsOwned(string id, string userId);
    }
}
