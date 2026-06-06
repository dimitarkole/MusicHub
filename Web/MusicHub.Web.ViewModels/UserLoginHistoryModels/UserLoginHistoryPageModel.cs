namespace MusicHub.Web.ViewModels.UserLoginHistoryModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class UserLoginHistoryPageModel<T> : PageModel
    {
        public IEnumerable<T> UserLoginHistory { get; set; }
    }
}
