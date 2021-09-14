namespace MusicHub.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum Privacy
    {
        [Display(Name = "The entity is public")]
        Public = 0,
        [Display(Name = "Who has link could view entity")]
        Protected = 1,
        [Display(Name = "Only creater could view entity")]
        Private = 2,
    }
}
