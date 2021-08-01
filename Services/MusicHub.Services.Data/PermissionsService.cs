namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PermissionModels;

    public class PermissionsService : IPermissionsService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public PermissionsService(ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public IEnumerable<T> All<T>()
             => this.context.Permissions
                 .To<T>()
                 .ToList();

        public async Task<int> Create(PermissionCreateInputModel model)
        {
            var permission = model.To<Permission>();
            await this.context.Permissions.AddAsync(permission);
            await this.context.SaveChangesAsync();
            return permission.Id;
        }

        public async Task Delete(int id)
        {
            var permission = this.context.Permissions.Find(id);
            this.context.Permissions.Remove(permission);
            await this.context.SaveChangesAsync();
        }

        public T GetById<T>(int id)
            => this.context.Permissions
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefault();

        public async Task Update(int id, PermissionEditInputModel model)
        {
            Permission permission = this.context.Permissions.Find(id);
            model.To<Permission>(permission);

            this.context.Permissions.Update(permission);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<PermissionViewModel> GetPermissions(string roleName)
        {
            List<PermissionViewModel> rolePermissions = this.context.RolePermissions
                .Include(rp => rp.Role)
                .Where(rp => rp.Role.Name == roleName)
                .Select(rp => new PermissionViewModel
                {
                    Description = rp.Permission.Description,
                    Id = rp.Permission.Id,
                    Type = rp.Permission.Type.ToString(),
                })
                .ToList();

            return rolePermissions;
        }

        public IEnumerable<PermissionViewModel> GetUserPermissions(string userId)
        {
            var userRoles = this.context.UserRoles
                .Where(ur => ur.UserId == userId);

            List<PermissionViewModel> rolePermissions = this.context.RolePermissions
                 .Include(rp => rp.Role)
                 .Where(rp => userRoles.Any(ur => ur.RoleId == rp.RoleId))
                 .Select(rp => new PermissionViewModel
                 {
                     Description = rp.Permission.Description,
                     Id = rp.Permission.Id,
                     Type = rp.Permission.Type.ToString(),
                 })
                 .ToList();

            return rolePermissions;
        }
    }
}
