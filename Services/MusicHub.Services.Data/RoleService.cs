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
    using MusicHub.Web.ViewModels.RolePermissionModels;

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

        public ICollection<T> All<T>()
            => this.context.Roles
                .OrderBy(r => r.Name)
                .To<T>()
                .ToList();

        public RolePageModel<T> All<T>(int page, int entitesPerPage)
        {
            var roles = this.context.Roles
                  .OrderBy(r => r.Name)
                  .To<T>();

            return new RolePageModel<T>()
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
            if (model.PermissionIds != null)
            {
                await this.CreateRolePermission(model.PermissionIds, role.Id);
            }

            await this.context.SaveChangesAsync();
            return role.Id;
        }

        public async Task Delete(string id)
        {
            var role = this.context.Roles.Find(id);
            var rolePermissions = this.context.RolePermissions
                .Where(rp => rp.RoleId == id)
                .ToList();
            this.context.RolePermissions.RemoveRange(rolePermissions);
            this.context.Roles.Remove(role);
            await this.context.SaveChangesAsync();
        }

        public RolePageModel<T> Filter<T>(int page, int entitesPerPage, RoleFilter filter)
        {
            IQueryable<Role> roles = this.context.Roles;
            roles = this.FilteringData(filter, roles);
            roles = this.OrederSearchData(filter.OrderMethod, roles);
            return new RolePageModel<T>()
            {
                Roles = roles
                    .To<T>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(roles.Count(), entitesPerPage),
            };
        }

        public RoleDetailsModel GetById(string id)
        {
            var result = new RoleDetailsModel();
            result.Name = this.context.Roles
                .Where(r => r.Id == id)
                .FirstOrDefault()
                .Name;
            result.UsersCount = this.context.UserRoles
                .Where(ur => ur.RoleId == id)
                .Count();
            result.Permissions = this.context.RolePermissions
              .Where(ur => ur.RoleId == id)
              .To<RolePermissionViewModel>()
              .ToList();
            return result;
        }

        public async Task Update(string id, RoleEditInputModel model)
        {
            Role role = this.context.Roles.Find(id);
            role.Name = model.Name;

            var rolePermissions = this.context.RolePermissions
                .Where(rp => rp.RoleId == id)
                .ToList();
            this.context.Roles.Update(role);
            this.context.RolePermissions.RemoveRange(rolePermissions);
            await this.context.SaveChangesAsync();
            if (model.PermissionIds != null)
            {
                await this.CreateRolePermission(model.PermissionIds, role.Id);
            }

            await this.context.SaveChangesAsync();
        }

        private async Task CreateRolePermission(ICollection<RolePermissionCreateInputModel> permissionIds, string roleId)
        {
            foreach (var permissionId in permissionIds)
            {
                var rolePemission = permissionId.To<RolePermission>();
                rolePemission.RoleId = roleId;
                await this.context.RolePermissions.AddAsync(rolePemission);
            }
        }

        private IQueryable<Role> FilteringData(RoleFilter filter, IQueryable<Role> roles)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                roles = roles.Where(r => r.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            return roles;
        }

        private IQueryable<Role> OrederSearchData(OrderMethod orderMethod, IQueryable<Role> roles)
        {
            if (orderMethod == OrderMethod.NameAsc)
            {
                roles = roles.OrderBy(r => r.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                roles = roles.OrderByDescending(r => r.Name);
            }

            return roles;
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
