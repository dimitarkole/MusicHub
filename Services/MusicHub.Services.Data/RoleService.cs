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
            await this.CreateRolePermission(model.PermissionIds, role.Id);
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

        public T GetById<T>(string id)
            => this.context.Roles
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefault();

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
            await this.CreateRolePermission(model.PermissionIds, role.Id);
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
