using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Common
{
    public enum VerificationType
    {
        [Display(Name = "Confirm email")]
        ConfirmEmail = 0,
        [Display(Name = "Change password")]
        ChangePassword = 1,
        [Display(Name = "Two factor login")]
        TwoFactorLogin = 2,
    }
}
