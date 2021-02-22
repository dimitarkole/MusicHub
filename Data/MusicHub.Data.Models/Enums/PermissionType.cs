namespace MusicHub.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum PermissionType
    {
        [Display(Name = "User administration - add, edit, deactivate users")]
        UserAdministration = 1,
        [Display(Name = "Manager license - active/deactive music licenses")]
        ManageLicense = 2,
        [Display(Name = "Data analisator")]
        DataAnalisator = 3,
        [Display(Name = "Use - add/edit/view licenses/musics/playlists/comments")]
        EditMatterInfo = 4,
    }
}
