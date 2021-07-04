using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicHub.Web.ViewModels.RoleModels;

namespace MusicHub.Services.Data.Interfaces
{
    public interface IRoleService
    {
        ICollection<T> All<T>();

        RolesAllViewModel<T> All<T>(int page, int entitesPerPage);

        RolesAllViewModel<T> Filter<T>(int page, int entitesPerPage, RoleFilter filter);

        Task<string> Create(RoleCreateInputModel model);

        T GetById<T>(string id);

        Task Update(string id, RoleEditInputModel model);

        Task Delete(string id);
    }
}
