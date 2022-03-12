namespace MusicHub.Tests.TestData
{
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.CategoryModels;
    using System.Collections.Generic;

    public static class CategoryTestsData
    {
        public static CategoryCreateInputModel CreateModel => new CategoryCreateInputModel()
        {
            Name = "asd",
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
            new Category { Id = "2", Name ="asd2"},
            new Category { Id = "1", Name ="asd1"},
            new Category { Id = "4", Name ="asd4"},
            new Category { Id = "3", Name ="asd3"},
            new Category { Id = "5", Name ="asd5"},
        };

        public static CategoryEditModel UpadateModel => new CategoryEditModel()
        {
            Name = "asd Update",
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
