using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.FollowModels
{
    public class FollowingsAllViewModel<T>
    {
        public IEnumerable<T> Followings { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
