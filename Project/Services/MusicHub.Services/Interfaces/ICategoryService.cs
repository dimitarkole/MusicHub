namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.CategoryModels;

    public interface ICategoryService
    {
        IList<T> All<T>();

        Task Create(CategoryCreateInputModel model);

        Task Update(string id, CategoryEditModel model);

        T GetById<T>(string id);

        Task Delete(string id);
    }
}
