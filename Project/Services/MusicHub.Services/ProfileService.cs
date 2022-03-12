namespace MusicHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MusicHub.Common;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.ProfilleModels;
    using Microsoft.EntityFrameworkCore;

    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext context;

        public ProfileService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool UserExistsByUsername(string username, string id = null) =>
            this.context.Users.Any(u => u.UserName == username && u.Id != id);

        public bool UserExistsByEmail(string email, string id = null) =>
            this.context.Users.Any(u => u.Email == email && u.Id != id);

        public IEnumerable<string> GetFullNames(IEnumerable<string> userIds) =>
            this.context.Users
                .Where(u => userIds.Contains(u.Id))
                .Select(u => $"{u.FirstName} {u.LastName}")
                .ToList();

        public Task Delete(string id) => this.UpdateIsDeleted(id, true);

        public Task Renew(string id) => this.UpdateIsDeleted(id, false);

        public IEnumerable<T> All<T>() => this.context
            .Users
            .OrderBy(u => u.UserName)
            .To<T>()
            .ToList();

        public T GetById<T>(string id) =>
            this.context.Users
                .Where(u => u.Id == id)
                .To<T>()
                .FirstOrDefault();

        public IEnumerable<ProfileViewModel> Search(string searchQuery)
        {
            var users = this.context.Users
                .Where(u => u.FirstName.Contains(searchQuery) ||
                            u.LastName.Contains(searchQuery))
                .To<ProfileViewModel>()
                .ToList();

            return users;
        }

        public IEnumerable<ProfileViewModel> Search(UserFilter model)
        {
            IQueryable<ApplicationUser> users = this.context.Users;

            if (!string.IsNullOrEmpty(model.Id))
            {
                users = users.Where(u => u.Id == model.Id);
            }

            if (!string.IsNullOrEmpty(model.LastName))
            {
                users = users.Where(u => u.LastName.Contains(model.LastName));
            }

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                users = users.Where(u => u.FirstName.Contains(model.FirstName));
            }

            if (!string.IsNullOrEmpty(model.UserName))
            {
                users = users.Where(u => u.UserName == model.UserName);
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                users = users.Where(u => u.Email == model.Email);
            }

            return users.To<ProfileViewModel>().ToList();
        }

        /*public bool HasPermissions(string id, PermissionType permissionType)
        {
            IQueryable<string> userRolesIds = this.context.Roles
                .Where(ur => ur.Id == id)
                .Select(ur => ur.Id);

            bool hasPermission = this.context.Roles
                 .Any(r => userRolesIds.Contains(r.Id) &&
                    r.Any(p => p.Permission.Type == permissionType));

            return hasPermission;
        }*/

        public async Task SetTimeZone(string timeZoneId, string userId)
        {
            ApplicationUser user = this.context.Users.Find(userId);
            user.TimeZoneId = timeZoneId;
            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();
        }

        public async Task ToggleSetting(int settingId)
        {
            UserSetting setting = this.context.UserSettings.Find(settingId);
            setting.Enabled = !setting.Enabled;
            this.context.UserSettings.Update(setting);
            await this.context.SaveChangesAsync();
        }

        public bool SettingEnabled(SettingType settingType, string userId) =>
            this.context.UserSettings
            .Any(s => s.Type == settingType && s.UserId == userId && s.Enabled);

        public IEnumerable<T> GetUsersInfo<T>(IEnumerable<string> userIds) =>
            this.context.Users
            .Where(u => userIds.Contains(u.Id))
            .OrderBy(u => u.UserName)
            .To<T>()
            .ToList();

        public async Task UploadProfileImage(string userId, UserProfilePictueInputModel model)
        {
            ApplicationUser user = this.context.Users.Find(userId);
            user.ImageUrl = model.ImageUrl;

            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();
        }

        public async Task AddDefaultSettings(string userId)
        {
            foreach (SettingType settingType in Enum.GetValues(typeof(SettingType)))
            {
                await this.context.UserSettings.AddAsync(new UserSetting
                {
                    Enabled = true,
                    Type = settingType,
                    UserId = userId,
                });
            }

            await this.context.SaveChangesAsync();
        }

        public IEnumerable<T> GetSettings<T>(string userId) =>
            this.context.UserSettings
            .Where(u => u.UserId == userId)
            .To<T>()
            .ToList();

        public async Task<ApplicationUser> Update(string id, ProfileEditModel model)
        {
            ApplicationUser user = this.context.Users.Find(id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Birthday = model.Birthday;

            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();
            return user;
        }

        public Task SetTimeZone(int timeZoneId, string userId)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateIsDeleted(string id, bool isDeleted)
        {
            ApplicationUser user = this.context.Users.Find(id);
            user.IsDeleted = isDeleted;
            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();
        }

        /* public IEnumerable<PermissionType> GetPermissions(IEnumerable<string> roles) =>
             context.Roles
                 .Where(r => roles.Contains(r.Name))
                 .SelectMany(r => r.Permissions.Select(p => p.Permission.Type))
                 .Distinct()
                 .ToList();*/
    }
}
