namespace MusicHub.Web.ViewModels.PermissionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class PermissionPageModel<T> : PageModel
    {
        public IEnumerable<T> Permissions { get; set; }
    }
}
