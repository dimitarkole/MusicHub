namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.CategoryModels;

    public interface ICategoryService
    {
        CategoriesAllViewModel<T> All<T>(int page, int entitesPerPage);

        IList<T> All<T>();

        Task Create(CategoryCreateInputModel model);

        Task Update(string id, CategoryEditModel model);

        T GetById<T>(string id);

        Task Delete(string id);

        CategoriesAllViewModel<T> Filter<T>(int page, int entitesPerPage, CategoryFilterInputModel filter);
    }
}
