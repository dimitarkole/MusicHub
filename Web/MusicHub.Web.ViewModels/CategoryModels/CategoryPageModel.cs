namespace MusicHub.Web.ViewModels.CategoryModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class CategoryPageModel<T> : PageModel
    {
        public IEnumerable<T> Categories { get; set; }
    }
}
