namespace MusicHub.Web.ViewModels.NotificationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class NotificationPageModel<T>
    {
        public IEnumerable<T> Notifications { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
