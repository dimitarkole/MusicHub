using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models.Enums
{
    public enum NotificationType
    {
        [Display(Name = "NewFollow - New follow notification")]
        NewFollow = 0,
        [Display(Name = "Like music")]
        LikeMusic = 1,
        [Display(Name = "Like comment")]
        LikeComment = 2,
        [Display(Name = "New comment")]
        NewComment = 3,
    }
}
