namespace MusicHub.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class UserPageModel<T> : PageModel
    {
        public IEnumerable<T> Users { get; set; }
    }
}
