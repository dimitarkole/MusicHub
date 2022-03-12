namespace MusicHub.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum MusicLicenseType
    {
        [Display(Name = "CC - Creative Commons license")]
        CC = 0,
        [Display(Name = "CC-BY - Creative Commons license with attribution")]
        CCBY = 1,
        [Display(Name = "CC-BY-SA - Creative Commons license with attribution and shake-alike")]
        CCBYSA = 2,
        [Display(Name = "CC-BY-SA - Creative Commons license with attribution and shake-alike")]
        CCBYNDSA = 3,
    }
}
