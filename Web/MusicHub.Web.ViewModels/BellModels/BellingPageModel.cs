namespace MusicHub.Web.ViewModels.BellModels
{
    using MusicHub.Web.ViewModels.CommonModels;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BellingPageModel<T> : PageModel
    {
        public IEnumerable<T> Bellings { get; set; }
    }
}
