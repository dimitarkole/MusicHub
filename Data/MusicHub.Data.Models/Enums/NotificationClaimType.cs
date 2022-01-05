using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models.Enums
{
    public enum NotificationClaimType
    {
        [Display(Name = "UserId - Id of user")]
        UserId = 0,
        [Display(Name = "MusicId - Id of music")]
        MusicId = 1,
        [Display(Name = "PlaylistId - Id of playlist")]
        PlaylistId = 2,
        [Display(Name = "PlaylistId - Id of playlist")]
        CommentId = 3,
    }
}
