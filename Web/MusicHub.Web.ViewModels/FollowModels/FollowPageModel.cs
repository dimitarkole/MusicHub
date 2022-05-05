namespace MusicHub.Web.ViewModels.FollowModels
{
    using MusicHub.Web.ViewModels.CommonModels;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FollowPageModel<T> : PageModel
    {
        public IEnumerable<T> Followers { get; set; }
    }
}
