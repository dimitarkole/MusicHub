namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.LicenseModels;

    public interface ILicenseService
    {
        LicenseAllViewModel<T> All<T>(int page, int entitesPerPage);

        LicenseAllViewModel<T> AllOwn<T>(int page, int entitesPerPage, string userId);

        ICollection<T> AllOwnApproved<T>(string userId);

        LicenseAllViewModel<T> Filter<T>(int page, int entitesPerPage, LicenseFilter filter, string userId = null);

        Task<string> Create(LicenseCreateInputModel model, string userId);

        T GetById<T>(string id);

        Task Update(string id, LicenseEditInputModel model);

        Task ChangeStatus(string id, LicenseStatusInputModel model);

        Task Delete(string id);

        bool IsOwn(string licenseId, string userId);

        Task CreateFile(LicenseFileCreateInputModel model);

        Task DeleteFile(string id);
    }
}
