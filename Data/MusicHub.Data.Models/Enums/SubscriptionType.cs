using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models.Enums
{
    public enum SubscriptionType
    {
        [Display(Name = "All")]
        All = 0,
        [Display(Name = "User")]
        User = 1,
        [Display(Name = "MusicCreator")]
        MusicCreator = 2,
    }
}
