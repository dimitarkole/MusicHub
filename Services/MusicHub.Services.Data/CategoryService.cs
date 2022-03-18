namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.CategoryModels;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public CategoryService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public CategoryPageModel<T> All<T>(int page, int entitesPerPage)
        {
            var categories = this.context.Categories
                    .OrderBy(c => c.Name)
                    .To<T>();
            return new CategoryPageModel<T>()
            {
                Categories = categories
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(categories.Count(), entitesPerPage),
            };
        }

        public CategoryPageModel<T> Filter<T>(int page, int entitesPerPage, CategoryFilterInputModel filter)
        {
            IQueryable<Category> categories = this.context.Categories;
            if (filter.Name != null)
            {
                categories = categories.Where(c => c.Name.Contains(filter.Name));
            }

            var categoriesT = categories.OrderBy(c => c.Name)
                    .To<T>();

            return new CategoryPageModel<T>()
            {
                Categories = categoriesT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(categoriesT.Count(), entitesPerPage),
            };
        }

        public IList<T> All<T>()
            => this.context.Categories
                    .OrderBy(c => c.Name)
                    .To<T>()
                    .ToList();

        public async Task Create(CategoryCreateInputModel model)
        {
            Category category = model.To<Category>();
            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            Category category = this.context.Categories.Find(id);
            this.context.Categories.Remove(category);
            await this.context.SaveChangesAsync();
        }

        public T GetById<T>(string id) =>
            this.context.Categories
            .Where(m => m.Id == id)
            .To<T>()
            .FirstOrDefault();

        public async Task Update(string id, CategoryEditModel model)
        {
            Category category = this.context.Categories
               .FirstOrDefault(s => s.Id == id);

            model.To<Category>(category);

            this.context.Categories.Update(category);
            await this.context.SaveChangesAsync();
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
