namespace MusicHub.Web.ViewModels.CommentModels
{
    using System.Collections.Generic;

    public class CommentsAllViewModel<T>
    {
        public IEnumerable<T> Comments { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
