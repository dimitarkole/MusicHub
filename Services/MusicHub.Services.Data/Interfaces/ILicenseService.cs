namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.LicenseModels;

    public interface ILicenseService
    {
        LicensePageModel<T> All<T>(int page, int entitesPerPage);

        ICollection<T> AllFiles<T>(string licenseId);

        LicensePageModel<T> AllOwned<T>(int page, int entitesPerPage, string userId);

        ICollection<T> AllOwnedApproved<T>(string userId);

        LicensePageModel<T> Filter<T>(int page, int entitesPerPage, LicenseFilter filter, string userId = null);

        Task<string> Create(LicenseCreateInputModel model, string userId);

        T GetById<T>(string id);

        Task Update(string id, LicenseEditInputModel model);

        Task ChangeStatus(string id, LicenseStatusInputModel model);

        Task Delete(string id);

        bool IsOwned(string licenseId, string userId);

        Task CreateFile(LicenseFileCreateInputModel model);

        Task DeleteFile(string id);
    }
}
