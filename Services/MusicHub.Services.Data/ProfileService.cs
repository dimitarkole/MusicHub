namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels;
    using MusicHub.Web.ViewModels.Users;

    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext context;

        public ProfileService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool UserExistsByUsername(string username, string id = null) =>
            context.Users.Any(u => u.UserName == username && u.Id != id);

        public bool UserExistsByEmail(string email, string id = null) =>
            context.Users.Any(u => u.Email == email && u.Id != id);

        public IEnumerable<string> GetFullNames(IEnumerable<string> userIds) =>
            context.Users
                .Where(u => !u.IsDeleted)
                .Where(u => userIds.Contains(u.Id))
                .Select(u => $"{u.FirstName} {u.LastName}")
                .ToList();

        public Task Delete(string id) => UpdateIsDeleted(id, true);

        public Task Renew(string id) => UpdateIsDeleted(id, false);

        public IEnumerable<T> All<T>() => context.Users.Where(u => !u.IsDeleted).To<T>().ToList();

        public T GetById<T>(string id) =>
            context.Users
            .Where(u => u.Id == id && !u.IsDeleted)
            .To<T>()
            .FirstOrDefault();

        public IEnumerable<UserViewModel> Search(string searchQuery)
        {
            var users = context.Users
                .Where(u => !u.IsDeleted)
                .Where(u => u.FirstName.Contains(searchQuery) ||
                            u.LastName.Contains(searchQuery))
                .To<UserViewModel>()
                .ToList();

            return users;
        }

        public IEnumerable<UserViewModel> Search(UserFilter model)
        {
            IQueryable<User> users = this.context.Users.Where(u => !u.IsDeleted);

            if (!string.IsNullOrEmpty(model.LastName))
            {
                users = users.Where(u => u.LastName.Contains(model.LastName));
            }
            if (!string.IsNullOrEmpty(model.FirstName))
            {
                users = users.Where(u => u.FirstName.Contains(model.FirstName));
            }

            return users.To<UserViewModel>().ToList();
        }

        public bool HasPermissions(string id, PermissionType permissionType)
        {
            IQueryable<string> userRolesIds = context.UserRoles
                .Where(ur => ur.UserId == id)
                .Select(ur => ur.RoleId);

            bool hasPermission = context.Roles
                 .Any(r => userRolesIds.Contains(r.Id) &&
                    r.Permissions.Any(p => p.Permission.Type == permissionType));

            return hasPermission;
        }

        public async Task<User> Update(string id, UserEditInputModel model)
        {
            User user = this.context.Users
                .FirstOrDefault(u => u.Id == id);

            model.To<User>(user);
            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        public async Task ToggleSetting(int settingId)
        {
            UserSetting setting = this.context.UserSettings.Find(settingId);
            setting.Enabled = !setting.Enabled;
            this.context.UserSettings.Update(setting);
            await this.context.SaveChangesAsync();
        }

        public bool SettingEnabled(SettingType settingType, string userId) =>
            context.UserSettings
            .Any(s => s.Type == settingType && s.UserId == userId && s.Enabled);

        public IEnumerable<T> GetUsersInfo<T>(IEnumerable<string> userIds) =>
            context.Users
            .Where(u => userIds.Contains(u.Id))
            .To<T>()
            .ToList();

        public async Task AddDefaultSettings(string userId)
        {
            foreach (SettingType settingType in Enum.GetValues(typeof(SettingType)))
            {
                await context.UserSettings.AddAsync(new UserSetting
                {
                    Enabled = true,
                    Type = settingType,
                    UserId = userId
                });
            }

            await context.SaveChangesAsync();
        }

        public IEnumerable<T> GetSettings<T>(string userId) =>
            context.UserSettings
            .Where(u => u.UserId == userId)
            .To<T>()
            .ToList();

        private async Task UpdateIsDeleted(string id, bool isDeleted)
        {
            User user = this.context.Users.Find(id);
            user.IsDeleted = isDeleted;
            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();
        }

        public async Task UploadProfileImage(string userId, UserProfilePictueInputModel model)
        {
            User user = context.Users.Find(userId);
            user.ImageUrl = model.ImageUrl;

            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
