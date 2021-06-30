namespace MusicHub.Web.ViewModels.PermissionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PermissionsAllViewModel<T>
    {
        public IEnumerable<T> Permissions { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
