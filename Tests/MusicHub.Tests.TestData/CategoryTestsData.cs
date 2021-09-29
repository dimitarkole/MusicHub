namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.CategoryModels;

    public static class CategoryTestsData
    {
        public static CategoryCreateInputModel CreateModel => new CategoryCreateInputModel()
        {
            Name = "category name",
        };

        public static CategoryFilterInputModel CreateFilter => new CategoryFilterInputModel()
        {
            Name = "a",
        };

        public static CategoryCreateInputModel CreateModelBadRequestRequared => new CategoryCreateInputModel()
        {
            Name = "",
        };

        public static CategoryCreateInputModel CreateModelBadRequestMinNameLenght => new CategoryCreateInputModel()
        {
            Name = "n",
        };

        public static CategoryCreateInputModel CreateModelBadRequestMaxNameLenght => new CategoryCreateInputModel()
        {
            Name = "simple category name with more carachers simple",
        };

        public static List<Category> GetCategories => new List<Category>()
        {
            new Category { Id = "categoryId1", Name = "category name1"},
            new Category { Id = "categoryId2", Name = "category name2"},
            new Category { Id = "categoryId3", Name = "category name3"},
            new Category { Id = "categoryId4", Name = "category name4"},
            new Category { Id = "categoryId5", Name = "category name5"},
        };

        public static CategoryEditModel UpadateModel => new CategoryEditModel()
        {
            Name = "Updated category name",
        };

        public static CategoryEditModel UpdateModelBadRequestRequared => new CategoryEditModel()
        {
            Name = "",
        };
        public static CategoryEditModel UpdateModelBadRequestMinNameLenght => new CategoryEditModel()
        {
            Name = "n",
        };

        public static CategoryEditModel UpdateModelBadRequestMaxNameLenght => new CategoryEditModel()
        {
            Name = "simple category name with more carachers simple",
        };

        public static string UpdateCategoryId => GetCategories[0].Id;

        public static string DeleteCategoryId => GetCategories[0].Id;

        public static string GetCategoryId => GetCategories[0].Id;
    }
}
