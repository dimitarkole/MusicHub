namespace MusicHub.Web.ViewModels.PermissionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PermissionPageModel<T>
    {
        public IEnumerable<T> Permissions { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
