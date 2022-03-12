using MusicHub.Common;
using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.CategoryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class CategoryControllerTests
    {
        [Fact]
        public void Create_WhitCurrectData_ShouldReturnOk() 
            =>  MyController<CategoryController>
            .Instance()
            .Calling(c => c.Post(CategoryTestsData.CreateModel))
            .ShouldReturn()
            .StatusCode(StatusCodes.Status201Created);

        [Fact]
        public void Create_WhitCurrectData_ShouldReturnBadRequestRequared()
        {
            var model = CategoryTestsData.CreateModelBadRequestRequared;
            MyController<CategoryController>
               .Instance()
               .Calling(c => c.Post(model))
               .ShouldReturn()
               .BadRequest(result => result
                    .WithErrorOfType<List<string>>()
                    .Passing(actualContacts =>
                    {
                        var nameMaxLength = ModelConstants.CategotyConstants.NameMaxLength;
                        var nameMinLength = ModelConstants.CategotyConstants.NameMinLength;
                        if (model.Name.Length < nameMinLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a minimum length of '{nameMinLength}'.", actualContacts);
                        }
                        else if (model.Name.Length > nameMaxLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a maximum length of '{nameMaxLength}'.", actualContacts);
                        }
                        else if (model.Name.Length == 0)
                        {
                            Assert.Contains($"Addresses must be between  and ", actualContacts);
                        }
                    }));
        }

        [Fact]
        public void Create_WhitCurrectData_ShouldReturnBadRequestMinNameLenght()
        {
            var model = CategoryTestsData.CreateModelBadRequestMinNameLenght;
            MyController<CategoryController>
               .Instance()
               .Calling(c => c.Post(model))
               .ShouldReturn()
               .BadRequest(result => result
                    .WithErrorOfType<List<string>>()
                    .Passing(actualContacts =>
                    {
                        var nameMaxLength = ModelConstants.CategotyConstants.NameMaxLength;
                        var nameMinLength = ModelConstants.CategotyConstants.NameMinLength;
                        if (model.Name.Length < nameMinLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a minimum length of '{nameMinLength}'.", actualContacts);
                        }
                        else if (model.Name.Length > nameMaxLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a maximum length of '{nameMaxLength}'.", actualContacts);
                        }
                        else if (model.Name.Length == 0)
                        {
                            Assert.Contains($"Addresses must be between  and ", actualContacts);
                        }
                    }));
        }


        [Fact]
        public void Create_WhitCurrectData_ShouldReturnBadRequestMaxNameLenght()
        {
            var model = CategoryTestsData.CreateModelBadRequestMaxNameLenght;
            MyController<CategoryController>
               .Instance()
               .Calling(c => c.Post(model))
               .ShouldReturn()
               .BadRequest(result => result
                    .WithErrorOfType<List<string>>()
                    .Passing(actualContacts =>
                    {
                        var nameMaxLength = ModelConstants.CategotyConstants.NameMaxLength;
                        var nameMinLength = ModelConstants.CategotyConstants.NameMinLength;
                        if (model.Name.Length < nameMinLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a minimum length of '{nameMinLength}'.", actualContacts);
                        }
                        else if (model.Name.Length > nameMaxLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a maximum length of '{nameMaxLength}'.", actualContacts);
                        }
                        else if (model.Name.Length == 0)
                        {
                            Assert.Contains($"Addresses must be between  and ", actualContacts);
                        }
                    }));
        }

        [Fact]
        public void All_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            IList<Category> categories = CategoryTestsData.GetCategories;

            MyController<CategoryController>
            .Instance()
            .WithData(categories)
            .Calling(c => c.Get())
            .ShouldReturn()
            .Ok(result => result
                .WithModelOfType<IList<CategoryViewModel>>()
                .Passing(actualCategories =>
                {
                    Assert.Equal(categories.Count(), actualCategories.Count());
                    for (int i = 0; i < categories.Count; i++)
                    {
                        Assert.Contains(actualCategories, c => c.Id== categories[i].Id
                            && c.Name == categories[i].Name);
                    }
                }));
        }

        [Fact]
        public void Get_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            IList<Category> categories = CategoryTestsData.GetCategories;
            var category = categories[0];
            MyController<CategoryController>
            .Instance()
            .WithData(categories)
            .Calling(c => c.Get(categories[0].Id))
            .ShouldReturn()
            .Ok(result => result
                .WithModelOfType<CategoryViewModel>()
                .Passing(actualCategory =>
                {
                    Assert.Equal(category.Id, actualCategory.Id);
                    Assert.Equal(category.Name, actualCategory.Name);
                }));
        }

        [Fact]
        public void Delete_WithDataInTheDb_ShouldReturnOk()
        {
            IList<Category> categories = CategoryTestsData.GetCategories;
            var category = categories[0];
            MyController<CategoryController>
                .Instance()
                .WithData(categories)
                .Calling(c => c.Delete(categories[0].Id))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void Edit_WhitCurrectData_ShouldReturnOk()
        {
            IList<Category> categories = CategoryTestsData.GetCategories;
            var editCategoryId = categories[0].Id;
            var model = CategoryTestsData.UpadateModel;
            MyController<CategoryController>
                .Instance()
                 .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, "1"))
                .Calling(c => c.Put(editCategoryId, model))
                .ShouldReturn()
                .Ok();
        }


        [Fact]
        public void Edit_WhitCurrectData_ShouldReturnBadRequestRequared()
        {
            IList<Category> categories = CategoryTestsData.GetCategories;
            var editCategoryId = categories[0].Id;
            var model = CategoryTestsData.UpdateModelBadRequestRequared;
            MyController<CategoryController>
               .Instance()
               .Calling(c => c.Put(editCategoryId, model))
               .ShouldReturn()
               .BadRequest(result => result
                    .WithErrorOfType<List<string>>()
                    .Passing(actualContacts =>
                    {
                        var nameMaxLength = ModelConstants.CategotyConstants.NameMaxLength;
                        var nameMinLength = ModelConstants.CategotyConstants.NameMinLength;
                        if (model.Name.Length < nameMinLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a minimum length of '{nameMinLength}'.", actualContacts);
                        }
                        else if (model.Name.Length > nameMaxLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a maximum length of '{nameMaxLength}'.", actualContacts);
                        }
                        else if (model.Name.Length == 0)
                        {
                            Assert.Contains($"Addresses must be between  and ", actualContacts);
                        }
                    }));
        }

        [Fact]
        public void Edit_WhitCurrectData_ShouldReturnBadRequestMinNameLenght()
        {
            IList<Category> categories = CategoryTestsData.GetCategories;
            var editCategoryId = categories[0].Id;
            var model = CategoryTestsData.UpdateModelBadRequestMinNameLenght;
            MyController<CategoryController>
               .Instance()
               .Calling(c => c.Put(editCategoryId, model))
               .ShouldReturn()
               .BadRequest(result => result
                    .WithErrorOfType<List<string>>()
                    .Passing(actualContacts =>
                    {
                        var nameMaxLength = ModelConstants.CategotyConstants.NameMaxLength;
                        var nameMinLength = ModelConstants.CategotyConstants.NameMinLength;
                        if (model.Name.Length < nameMinLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a minimum length of '{nameMinLength}'.", actualContacts);
                        }
                        else if (model.Name.Length > nameMaxLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a maximum length of '{nameMaxLength}'.", actualContacts);
                        }
                        else if (model.Name.Length == 0)
                        {
                            Assert.Contains($"Addresses must be between  and ", actualContacts);
                        }
                    }));
        }


        [Fact]
        public void Update_WhitCurrectData_ShouldReturnBadRequestMaxNameLenght()
        {
            IList<Category> categories = CategoryTestsData.GetCategories;
            var editCategoryId = categories[0].Id;
            var model = CategoryTestsData.UpdateModelBadRequestMaxNameLenght;
            MyController<CategoryController>
               .Instance()
               .Calling(c => c.Put(editCategoryId, model))
               .ShouldReturn()
               .BadRequest(result => result
                    .WithErrorOfType<List<string>>()
                    .Passing(actualContacts =>
                    {
                        var nameMaxLength = ModelConstants.CategotyConstants.NameMaxLength;
                        var nameMinLength = ModelConstants.CategotyConstants.NameMinLength;
                        if (model.Name.Length < nameMinLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a minimum length of '{nameMinLength}'.", actualContacts);
                        }
                        else if (model.Name.Length > nameMaxLength)
                        {
                            Assert.Contains($"The field Name must be a string or array type with a maximum length of '{nameMaxLength}'.", actualContacts);
                        }
                        else if (model.Name.Length == 0)
                        {
                            Assert.Contains($"Addresses must be between  and ", actualContacts);
                        }
                    }));
        }
    }
}
