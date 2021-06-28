namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
    using MusicHub.Web.ViewModels.PermissionModels;
    using MusicHub.Web.ViewModels.RoleModels;

    using static MusicHub.Common.GlobalConstants;

    [Authorize]
    [Permission(PermissionType.ManageUserPermissions)]
    public class RoleController : ApiController
    {
        private readonly IRoleService roleService;

        public RoleController(
            IRoleService roleService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoleCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var roleId = await this.roleService.Create(model);
            return this.StatusCode(StatusCodes.Status201Created, new { roleId = roleId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, RoleEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.roleService.Update(id, model);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.roleService.Delete(id);
            return this.Ok();
        }

        [HttpGet(nameof(Get) + "/{page}")]
        public ActionResult<RolesAllViewModel<RoleViewModel>> Get(int page)
            => this.Ok(this.roleService.All<RoleViewModel>(page, PaginationData.RolesPerPage));

        [HttpPost]
        [Route(nameof(Filter) + "/{page}")]
        public ActionResult<RolesAllViewModel<RoleViewModel>> Filter(int page, RoleFilter filter)
          => this.Ok(this.roleService.Filter<RoleViewModel>(page, PaginationData.RolesPerPage, filter));

        [HttpGet("{id}")]
        public ActionResult<RoleDetailsModel> Get(string id)
            => this.Ok(this.roleService.GetById<RoleDetailsModel>(id));
    }
}
