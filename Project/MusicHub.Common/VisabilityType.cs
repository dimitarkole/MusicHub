namespace MusicHub.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum VisabilityType
    {
        [Display(Name = "Public")]
        Public = 1,
        [Display(Name = "All subsriber")]
        Subsriber = 2,
        [Display(Name = "Only me")]
        OnlyMe = 3,
    }
}
