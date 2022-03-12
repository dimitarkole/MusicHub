namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.CategoryModels;
    using Xunit;

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
            int expextedCount = this.context.Categories.Count();

            var results = categoryService.All<CategoryViewModel>().ToList();

            Assert.Equal(expextedCount, results.Count);
            foreach (var result in results)
            {
                Assert.True(this.context.Categories.Any(c => c.Id == result.Id
                    && c.Name == result.Name));
            }
        }

        [Fact]
        public async Task UpdateCategory_WithValidData_ShouldWorkCorrect()
        {
            var getCategory = CategoryTestsData.GetCategories;
            var categoryService = await this.CreateCategoryService(getCategory);
            var newModel = CategoryTestsData.UpadateModel;
            var updateItemId = CategoryTestsData.UpdateCategoryId;

            await categoryService.Update(updateItemId, newModel);

            Assert.True(this.context.Categories.Any(c => c.Id == updateItemId
                && c.Name == newModel.Name));
        }

        [Fact]
        public async Task DeleteCategory_WithValidData_ShouldWorkCorrect()
        {
            var getCategory = CategoryTestsData.GetCategories;
            var categoryService = await this.CreateCategoryService(getCategory);
            int expextedCount = this.context.Categories.Count();
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
            var service = new CategoryService(this.context);

            return service;
        }
    }
}
