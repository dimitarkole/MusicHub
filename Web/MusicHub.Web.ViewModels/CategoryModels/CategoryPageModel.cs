using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.CategoryModels
{
    public class CategoryPageModel<T>
    {
        public IEnumerable<T> Categories { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
