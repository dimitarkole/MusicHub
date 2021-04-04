namespace MusicHub.Web.ViewModels.VerificationCodeModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using static MusicHub.Common.GlobalConstants;

    public class VerificationCodeCheckInputModel
    {
        public string Code { get; set; }

        public string UserId { get; set; }
    }
}
