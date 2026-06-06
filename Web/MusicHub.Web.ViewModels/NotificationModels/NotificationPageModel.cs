namespace MusicHub.Web.ViewModels.NotificationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class NotificationPageModel<T> : PageModel
    {
        public IEnumerable<T> Notifications { get; set; }
    }
}
