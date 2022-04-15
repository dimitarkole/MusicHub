namespace MusicHub.Web.ViewModels.CommentModels
{
    using System.Collections.Generic;

    using MusicHub.Web.ViewModels.CommonModels;

    public class CommentPageModel<T> : PageModel
    {
        public IEnumerable<T> Comments { get; set; }
    }
}
