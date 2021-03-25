using MusicHub.Common.Extensions;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Seeding
{
    public class PermissionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var permissionTypesByDescription = EnumExtensions.GetEnumValuesByDisplayName<PermissionType>();
            foreach (var permissionTypeByDescription in permissionTypesByDescription)
            {
                PermissionType type = permissionTypeByDescription.Key;
                string description = permissionTypeByDescription.Value;

                if (!dbContext.Permissions.Any(p => p.Type == type))
                {
                    await dbContext.Permissions.AddAsync(new Permission
                    {
                        Type = type,
                        Description = description,
                    });
                }
            }
        }
    }
}
