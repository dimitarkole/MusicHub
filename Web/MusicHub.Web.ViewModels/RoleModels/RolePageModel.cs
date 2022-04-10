namespace MusicHub.Web.ViewModels.RoleModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class RolePageModel<T> : PageModel
    {
        public IEnumerable<T> Roles { get; set; }
    }
}
