namespace MusicHub.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum VisibleStatus
    {
        [Display(Name = "The entity is public")]
        Public = 0,
        [Display(Name = "Who has link could view entity")]
        Hidden = 1,
        [Display(Name = "Only creater could view entity")]
        OnlyMe = 2,

        Default = 0,
    }
}
