namespace MusicHub.Web.ViewModels.FollowModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FollowPageModel<T>
    {
        public IEnumerable<T> Followers { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
