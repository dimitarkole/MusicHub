namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.UserIpRulesModels;

    public interface IUserIpRuleService
    {
        UserIpRulePageModel<T> Filter<T>(int page, int entitesPerPage, UserIpRuleFilterInputModel model, string userId);

        Task<string> Create(UserIpRuleCreateInputModel model, string userId);

        Task Update(UserIpRuleEditInputModel model, string id);

        T GetById<T>(string id);

        Task Delete(string id);

        bool IsOwned(string id, string userId);

        bool HasAccess(string userId, string ipAddress);
    }
}
