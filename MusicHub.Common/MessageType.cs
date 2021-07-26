using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Common
{
    public enum MessageType
    {
        [Display(Name = "Confirm Email")]
        ConfirmEmail = 0,
    }
}
