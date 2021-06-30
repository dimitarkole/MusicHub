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
    using MusicHub.Web.Infrastucture.Extensions;
    using MusicHub.Web.ViewModels.PermissionModels;

    using static MusicHub.Common.GlobalConstants;

    public class PermissionsController : ApiController
    {
        private readonly IPermissionsService permissionsService;

        public PermissionsController(
            IPermissionsService permissionsService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.permissionsService = permissionsService;
        }

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpGet(nameof(All))]
        public ActionResult All()
            => this.Ok(this.permissionsService.All<PermissionViewModel>());

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpPost]
        public async Task<IActionResult> Post(PermissionCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var permissionId = await this.permissionsService.Create(model);
            return this.StatusCode(StatusCodes.Status201Created, new { permissionId = permissionId });
        }

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PermissionEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.permissionsService.Update(id, model);
            return this.Ok();
        }

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.permissionsService.Delete(id);
            return this.Ok();
        }

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpGet(nameof(Get) + "{id}")]
        public ActionResult<PermissionViewModel> Get(int id)
         => this.Ok(this.permissionsService.GetById<PermissionViewModel>(id));

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpGet(nameof(GetSuperAdminPermissions))]
        public ActionResult GetSuperAdminPermissions()
            => this.Ok(this.permissionsService.GetPermissions(Roles.SuperAdministrator));

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpGet(nameof(GetAdminPermissions))]
        public ActionResult GetAdminPermissions()
            => this.Ok(this.permissionsService.GetPermissions(Roles.Administrator));

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpGet(nameof(GetUserPermissions))]
        public ActionResult GetUserPermissions()
            => this.Ok(this.permissionsService.GetPermissions(Roles.User));

        [Authorize]
        [HttpGet(nameof(GetOwnPermissions))]
        public ActionResult GetOwnPermissions()
            => this.Ok(this.permissionsService.GetUserPermissions(this.userManager.GetUserId(this.User)));
    }
}
