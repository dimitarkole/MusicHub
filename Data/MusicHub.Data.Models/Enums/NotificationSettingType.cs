using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models.Enums
{
    public enum NotificationSettingType
    {
        [Display(Name = "Send email after notification is created")]
        SendEmail = 0,
        [Display(Name = "System notification")]
        SystemNofitication = 1,
        [Display(Name = "NewFollower - notification when somebody follow user")]
        NewFollower = 2,
        [Display(Name = "LikeMusic - notification when somebody like user's music")]
        LikeMusic = 3,
        [Display(Name = "LikeComment - notification when somebody like user's comment")]
        LikeComment = 4,
        [Display(Name = "LikePlaylist - notification when somebody like user's playlist")]
        LikePlaylist = 5,
        [Display(Name = "LikePlaylist - notification when somebody like user's playlist")]
        NewComment = 6,
        [Display(Name = "LikePlaylist - notification when somebody like user's playlist")]
        ResponseComment = 7,
    }
}
