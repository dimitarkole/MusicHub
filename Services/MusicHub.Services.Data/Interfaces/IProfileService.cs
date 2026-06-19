namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Web.ViewModels.Users;

    public interface IProfileService
    {
        UserPageModel<T> All<T>(int page, int entitesPerPage);

        T GetById<T>(string id);

        T GetByEmail<T>(string email);

        bool UserExistsByUsername(string username, string id = null);

        IEnumerable<string> GetFullNames(IEnumerable<string> userIds);

        Task Delete(string id);

        Task Renew(string id);

        UserPageModel<UserViewModel> Search(int page, int entitesPerPage, string searchQuery);

        UserPageModel<UserViewModel> Search(int page, int entitesPerPage, UserFilter model);

        bool HasPermissions(string id, PermissionType permissionType);

        bool UserExistsByEmail(string email, string id = null);

        bool UserExistsById(string id);

        Task<User> Update(string id, UserEditInputModel model);
        Task<User> Update(string id, UserEditModel model);

        User GetUser(string id);

        Task ToggleSetting(int settingId);

        bool SettingEnabled(SettingType settingType, string userId);

        IEnumerable<T> GetUsersInfo<T>(IEnumerable<string> userIds);

        Task AddDefaultSettings(string userId);

        IEnumerable<T> GetSettings<T>(string userId);

        Task UploadProfileImage(string userId, UserProfilePictueInputModel model);

        bool IsEmailConfirm(string username);

        Task ConfirmEmail(string userId);
    }
}
