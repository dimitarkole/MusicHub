using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicHub.Web.ViewModels.RoleModels;
using MusicHub.Web.ViewModels.RolePermissionModels;

namespace MusicHub.Services.Data.Interfaces
{
    public interface IRoleService
    {
        ICollection<T> All<T>();

        RolePageModel<T> All<T>(int page, int entitesPerPage);

        RolePageModel<T> Filter<T>(int page, int entitesPerPage, RoleFilter filter);

        Task<string> Create(RoleCreateInputModel model);

        RoleDetailsModel GetById(string id);

        Task Update(string id, RoleEditInputModel model);

        Task Delete(string id);
    }
}
