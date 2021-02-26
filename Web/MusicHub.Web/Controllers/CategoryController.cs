using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.Infrastucture;
using MusicHub.Web.Infrastucture.Extensions;
using MusicHub.Web.ViewModels.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHub.Web.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(
            ICategoryService categoryService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.categoryService = categoryService;
        }

        /// <summary>Get categories in data.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CategoryViewModel>> Get() => this.Ok(this.categoryService.All<CategoryViewModel>());

        /// <summary>Get category by id.</summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<CategoryViewModel> Get(string id) => this.Ok(this.categoryService.GetById<CategoryViewModel>(id));

        /// <summary>Add category in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>Name - category name.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Permission(PermissionType.UserAdministration)]

        public async Task<IActionResult> Post(CategoryCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            await this.categoryService.Create(model);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Edit category.</summary>
        /// <param name="model">
        /// <para>Name - category name.</para>
        /// </param>
        /// <param name="id">The id of the category.</param>
        /// <returns>Ok.</returns>
        [HttpPut("{id}")]
        [Permission(PermissionType.UserAdministration)]
        public async Task<IActionResult> Put(string id, CategoryEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            await this.categoryService.Update(id, model);
            return this.Ok();
        }

        /// <summary>Delete category.</summary>
        /// <param name="id">The id of the category.</param>
        /// <returns>Ok.</returns>
        [HttpDelete("{id}")]
        [Permission(PermissionType.UserAdministration)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.categoryService.Delete(id);
            return this.Ok();
        }
    }
}
