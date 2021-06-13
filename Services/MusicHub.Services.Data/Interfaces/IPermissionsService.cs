namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.PermissionModels;

    public interface IPermissionsService
    {
        IEnumerable<PermissionViewModel> GetPermissions(string roleName);

        IEnumerable<PermissionViewModel> GetUserPermissions(string userId);

        PermissionsAllViewModel<T> All<T>(int page, int entitesPerPage);

        Task<int> Create(PermissionCreateInputModel model);

        T GetById<T>(int id);

        Task Update(int id, PermissionEditInputModel model);

        Task Delete(int id);
    }
}
