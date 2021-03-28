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
        IEnumerable<T> All<T>();

        IEnumerable<PermissionViewModel> GetPermissions(string roleName);

        IEnumerable<PermissionViewModel> GetUserPermissions(string userId);
    }
}
