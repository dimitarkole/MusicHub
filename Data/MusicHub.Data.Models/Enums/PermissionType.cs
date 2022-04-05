namespace MusicHub.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum PermissionType
    {
        [Display(Name = "User administration - manage users")]
        UserAdministration = 1,
        [Display(Name = "Manager permission and role - manage role and permission")]
        ManageUserPermissions = 2,
        [Display(Name = "User FindAndReplaceUsers - manage users")]
        FindAndReplaceUsers = 3,
        [Display(Name = "Category administrator - create, edit and delet ecategory")]
        CategoryAdministrator = 4,
        [Display(Name = "License manager - appprove/reject music licenses")]
        LicenseManager = 5,
        [Display(Name = "Data analisator - view system data")]
        DataAnalisator = 6,
        [Display(Name = "Team manager - view team data")]
        TeamManager = 7,
        [Display(Name = "Music creator - create, edit and delete music")]
        MusicCreator = 8,
        [Display(Name = "License creator - create, edit and delete license")]
        LicenseCreator = 9,
        [Display(Name = "Playlist creator - create, edit and delete playlist and add/remove music to playlist")]
        PlaylistCreator = 10,
        [Display(Name = "Comment creator - create, edit and delete comment")]
        CommentCreator = 11,
        [Display(Name = "User - play musics and playlists")]
        User = 12,
        [Display(Name = "SubscriptionManager - managing with subscriptions")]
        SubscriptionManager = 13,
        [Display(Name = "Album creator - create, edit and delete album and add/remove music to album")]
        AlbumCreator = 14,
        [Display(Name = "Reported music viewer - view all reported music")]
        ReportedMusicViewer = 15,
    }
}
