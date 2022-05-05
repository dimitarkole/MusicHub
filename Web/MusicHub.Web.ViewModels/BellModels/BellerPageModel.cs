namespace MusicHub.Web.ViewModels.BellModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.CommonModels;

    public class BellerPageModel<T> : PageModel
    {
        public IEnumerable<T> Bellers { get; set; }
    }
}
