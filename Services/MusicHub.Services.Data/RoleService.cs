namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PermissionModels;
    using MusicHub.Web.ViewModels.RoleModels;

    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public RoleService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public RolesAllViewModel<T> All<T>(int page, int entitesPerPage)
        {
            var roles = this.context.Roles
                  .OrderBy(m => m.Name)
                  .To<T>();

            return new RolesAllViewModel<T>()
            {
                Roles = roles
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(roles.Count(), entitesPerPage),
            };
        }

        public async Task<string> Create(RoleCreateInputModel model)
        {
            var role = model.To<Role>();
            await this.context.Roles.AddAsync(role);
            await this.context.SaveChangesAsync();
            return role.Id;
        }

        public async Task Delete(string id)
        {
            var role = this.context.Roles.Find(id);
            this.context.Roles.Remove(role);
            await this.context.SaveChangesAsync();
        }

        public RolesAllViewModel<T> Filter<T>(int page, int entitesPerPage, RoleFilter filter)
        {
            IQueryable<Role> roles = this.context.Roles;
            roles = this.FilteringData(filter, roles);
            roles = this.OrederSearchData(filter.OrderMethod, roles);
            return new RolesAllViewModel<T>()
            {
                Roles = roles
                    .To<T>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(roles.Count(), entitesPerPage),
            };
        }

        public RoleDetailsModel GetById(string id, int page, int entitesPerPage)
        {
            var role = this.context.Roles
                .Where(s => s.Id == id)
                .FirstOrDefault();
            var allPermissions = this.context.Permissions
                .Where(p => p.Roles.Any(rp => rp.RoleId == role.Id))
                .To<PermissionViewModel>();

            var permissionsPagination = new PermissionsAllViewModel<PermissionViewModel>()
            {
                Permissions = allPermissions
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(allPermissions.Count(), entitesPerPage),
            };

            var result = new RoleDetailsModel()
            {
                Id = role.Id,
                Name = role.Name,
                PermissionsPagination = permissionsPagination,
            };
            return result;
        }

        public async Task Update(string id, RoleEditInputModel model)
        {
            Role role = this.context.Roles.Find(id);
            model.To<Role>(role);

            this.context.Roles.Update(role);
            await this.context.SaveChangesAsync();
        }

        private IQueryable<Role> FilteringData(RoleFilter filter, IQueryable<Role> roles)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                roles = roles.Where(s => s.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            return roles;
        }

        private IQueryable<Role> OrederSearchData(OrderMethod orderMethod, IQueryable<Role> roles)
        {
            if (orderMethod == OrderMethod.NameAsc)
            {
                roles = roles.OrderBy(s => s.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                roles = roles.OrderByDescending(s => s.Name);
            }

            return roles;
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
