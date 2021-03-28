namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
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
        }

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpGet]
        public ActionResult All() => this.Ok(this.permissionsService.All<PermissionViewModel>());

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpGet(nameof(GetSuperAdminPermissions))]
        public ActionResult GetSuperAdminPermissions() => this.Ok(this.permissionsService.GetPermissions(Roles.SuperAdministrator));

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpGet(nameof(GetAdminPermissions))]
        public ActionResult GetAdminPermissions() => this.Ok(this.permissionsService.GetPermissions(Roles.Administrator));

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpGet(nameof(GetUserPermissions))]
        public ActionResult GetUserPermissions() => this.Ok(this.permissionsService.GetPermissions(Roles.User));

        [Authorize]
        [HttpGet(nameof(GetMyPermissions))]
        public ActionResult GetMyPermissions() => this.Ok(this.permissionsService.GetUserPermissions(this.User.GetUserId()));
    }
}
