namespace MusicHub.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum LicenseStatus
    {
        [Display(Name = "The license is not viewed by the administrator")]
        WaitToBeView = 0,

        [Display(Name = "Admin approved license")]
        Approve = 1,

        [Display(Name = "Admin rejected license")]
        Reject = 2,
    }
}
