namespace MusicHub.Web.ViewModels.FollowModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class FollowingPageModel<T> : PageModel
    {
        public IEnumerable<T> Followings { get; set; }
    }
}
