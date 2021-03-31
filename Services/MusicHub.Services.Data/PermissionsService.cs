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
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PermissionModels;

    public class PermissionsService : IPermissionsService
    {
        private readonly ApplicationDbContext context;

        public PermissionsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> All<T>() => this.context.Permissions.To<T>().ToList();

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
