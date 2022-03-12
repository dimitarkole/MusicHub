namespace MusicHub.Common
{
    using System.ComponentModel.DataAnnotations;

    public enum PermissionType
    {
        [Display(Name = "Super Administration - add, edit, deactivate users, song category")]
        UserAdministration = 1,
        [Display(Name = "Users - add, edit, deactivate song")]
        ManageUserPermissions = 2,
    }
}
