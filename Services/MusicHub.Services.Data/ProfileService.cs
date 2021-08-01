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
    using MusicHub.Common.Extensions;
    using MusicHub.Services.Mapping;
    using MusicHub.Services;
    using MusicHub.Services.Interfaces;
    using MusicHub.Common;

    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public ProfileService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public bool UserExistsByUsername(string username, string id = null) =>
            context.Users.Any(u => u.UserName == username && u.Id != id);

        public bool UserExistsById(string id) =>
            context.Users.Any(u => u.Id == id);

        public bool UserExistsByEmail(string email, string id = null) =>
            context.Users.Any(u => u.Email == email && u.Id != id);

        public IEnumerable<string> GetFullNames(IEnumerable<string> userIds) =>
            context.Users
                .Where(u => !u.IsDeleted)
                .Where(u => userIds.Contains(u.Id))
                .Select(u => $"{u.FirstName} {u.LastName}")
                .ToList();

        public async Task ConfirmEmail(string id)
        {
            var user = this.context.Users
                .Where(u => u.Id == id)
                .FirstOrDefault();

            user.EmailConfirmed = true;
            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();
        }

        public Task Delete(string id) => UpdateIsDeleted(id, true);

        public Task Renew(string id) => UpdateIsDeleted(id, false);

        public UserAllViewModel<T> All<T>(int page, int entitesPerPage)
        {
            var users = this.context.Users.Where(u => !u.IsDeleted)
                .OrderBy(u => u.UserName)
                .To<T>();

            return new UserAllViewModel<T>()
            {
                Users = users
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(users.Count(), entitesPerPage),
            };
        }

        public T GetById<T>(string id) =>
            context.Users
            .Where(u => u.Id == id && !u.IsDeleted)
            .To<T>()
            .FirstOrDefault();

        public bool CheckVerificationCode(string userId, VerificationCodeInputModel model) =>
            this.context.Users
            .Any(u => u.Id == userId &&
                !u.IsDeleted &&
                u.VerificationCode == model.VerificationCode);

        public UserAllViewModel<UserViewModel> Search(int page, int entitesPerPage, string searchQuery)
        {
            var users = this.context.Users
                .Where(u => !u.IsDeleted)
                .Where(u => u.FirstName.Contains(searchQuery) ||
                            u.LastName.Contains(searchQuery))
                .OrderBy(u => u.UserName)
                .To<UserViewModel>();

            return new UserAllViewModel<UserViewModel>()
            {
                Users = users
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(users.Count(), entitesPerPage),
            };
        }

        public UserAllViewModel<UserViewModel> Search(int page, int entitesPerPage, UserFilter model)
        {
            IQueryable<User> users = this.context.Users.Where(u => !u.IsDeleted);

            if (!string.IsNullOrEmpty(model.LastName))
            {
                users = users.Where(u => u.LastName.Contains(model.LastName));
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                users = users.Where(u => u.Email.Contains(model.Email));
            }

            if (!string.IsNullOrEmpty(model.UserName))
            {
                users = users.Where(u => u.UserName.Contains(model.UserName));
            }

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                users = users.Where(u => u.FirstName.Contains(model.FirstName));
            }

            users = this.OrederSearch(model.OrderMethod, users);

            return new UserAllViewModel<UserViewModel>()
            {
                Users = users
                    .To<UserViewModel>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(users.Count(), entitesPerPage),
            };
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

        public User GetUser(string id)
            => this.context.Users
                  .FirstOrDefault(u => u.Id == id);

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

        public bool IsEmailConfirm(string username)
            => this.context.Users.Any(u => u.UserName == username
                && u.Email == username
                && u.EmailConfirmed == true
                && !u.IsDeleted);

        private IQueryable<User> OrederSearch(OrderMethod orderMethod, IQueryable<User> users)
        {
            if (orderMethod == OrderMethod.NameDesc)
            {
                users = users.OrderByDescending(s => s.UserName);
            }
            else if (orderMethod == OrderMethod.EmailAsc)
            {
                users = users.OrderBy(s => s.Email);
            }
            else if (orderMethod == OrderMethod.EmailDesc)
            {
                users = users.OrderByDescending(s => s.Email);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                users = users.OrderBy(s => s.FirstName + " " + s.LastName);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                users = users.OrderByDescending(s => s.FirstName + " " + s.LastName);
            }
            else
            {
                users = users.OrderBy(s => s.UserName);
            }

            return users;
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
             => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);

        public T GetByEmail<T>(string email)
            => this.context.Users
                .Where(u => u.Email == email
                    && !u.IsDeleted)
                .FirstOrDefault()
                .To<T>();
    }
}
