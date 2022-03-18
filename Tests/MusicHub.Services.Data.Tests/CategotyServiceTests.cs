namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.CategoryModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

    public class CategotyServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateCategory_WithValidData_ShouldWorkCorrect()
        {
            var categoryService = await this.CreateCategoryService(new List<Category>());
            var model = CategoryTestsData.CreateModel;

            await categoryService.Create(model);

            Assert.True(this.context.Categories.Any(c => c.Name == model.Name));
        }

        [Fact]
        public async Task AllCategories_WithValidData_ShouldWorkCorrect()
        {
            var getCategories = CategoryTestsData.GetCategories;
            var categoryService = await this.CreateCategoryService(getCategories);

            var actual = categoryService.All<CategoryViewModel>().ToList();

            var expected = this.context.Categories
                .OrderBy(c => c.Name)
                .To<CategoryViewModel>()
                .ToList();

            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task AllCategoriesByPage_WithValidData_ShouldWorkCorrect(int page)
        {
            var getCategories = CategoryTestsData.GetCategories;
            var categoryService = await this.CreateCategoryService(getCategories);
            var entitesPerPage = PaginationData.CategoriesPerPage;
            var actual = categoryService.All<CategoryViewModel>(page, entitesPerPage);

            var categories = this.context.Categories
                .OrderBy(c => c.Name)
                .To<CategoryViewModel>()
                .ToList();

            var expected = new CategoryPageModel<CategoryViewModel>()
            {
                Categories = categories
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(categories.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Filter_WithValidData_ShouldWorkCorrect(int page)
        {
            var getCategories = CategoryTestsData.GetCategories;
            var filter = CategoryTestsData.CreateFilter;
            var categoryService = await this.CreateCategoryService(getCategories);
            var entitesPerPage = PaginationData.CategoriesPerPage;
            var actual = categoryService.Filter<CategoryViewModel>(page, entitesPerPage, filter);

            IQueryable<Category> categories = this.context.Categories
                .OrderBy(c => c.Name);

            if (filter.Name != null)
            {
                categories = categories.Where(c => c.Name.Contains(filter.Name));
            }

            var expected = new CategoryPageModel<CategoryViewModel>()
            {
                Categories = categories
                    .To<CategoryViewModel>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(categories.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task UpdateCategory_WithValidData_ShouldWorkCorrect()
        {
            var getCategory = CategoryTestsData.GetCategories;
            var categoryService = await this.CreateCategoryService(getCategory);
            var model = CategoryTestsData.UpadateModel;
            var updateItemId = CategoryTestsData.UpdateCategoryId;

            await categoryService.Update(updateItemId, model);

            Assert.True(this.context.Categories.Any(c => c.Id == updateItemId
                && c.Name == model.Name));
        }

        [Fact]
        public async Task DeleteCategory_WithValidData_ShouldWorkCorrect()
        {
            var getCategory = CategoryTestsData.GetCategories;
            var categoryService = await this.CreateCategoryService(getCategory);
            int expectedCount = this.context.Categories.Count();
            var categoryId = CategoryTestsData.DeleteCategoryId;

            await categoryService.Delete(categoryId);

            Assert.False(this.context.Categories.Any(c => c.Id == categoryId));
        }

        [Fact]
        public async Task GetCategoryById_WithValidData_ShouldWorkCorrect()
        {
            var getCategory = CategoryTestsData.GetCategories;
            var categoryService = await this.CreateCategoryService(getCategory);
            var categoryId = CategoryTestsData.GetCategoryId;

            var result = categoryService.GetById<CategoryViewModel>(categoryId);

            Assert.True(this.context.Categories.FirstOrDefault(c => c.Id == categoryId).Name == result.Name);
        }

        private async Task<CategoryService> CreateCategoryService(List<Category> categories)
        {
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.SaveChangesAsync();
            var service = new CategoryService(this.context, new PaginationService());

            return service;
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }
    }
}
