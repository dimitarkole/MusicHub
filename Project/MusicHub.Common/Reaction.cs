namespace MusicHub.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum Reaction
    {
        [Display(Name = "User not reaction")]
        None = 0,
        [Display(Name = "User like")]
        Like = 1,
        [Display(Name = "Users dislike")]
        Dislike = 2,

        Default = 0,
    }
}
