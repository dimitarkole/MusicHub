using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Data.Analysis.Interfaces;
using MusicHub.Web.Infrastucture;
using MusicHub.Web.ViewModels.ChartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHub.Web.Controllers.AnalysisControlles
{
   /* [Authorize]
    [Permission(PermissionType.ManageUserPermissions)] */
    public class RoleAnalysisController : ApiController
    {
        private readonly IRoleAnalysisService roleAnalysisService;

        public RoleAnalysisController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger,
            IRoleAnalysisService roleAnalysisService)
            : base(userManager, signInManager, logger)
        {
            this.roleAnalysisService = roleAnalysisService;
        }

        [HttpGet(nameof(GetRolesByCountOfUsers))]
        public ActionResult<PieChartViewModel> GetRolesByCountOfUsers()
             => this.Ok(this.roleAnalysisService.GetRolesByCountOfUsers());

        [HttpGet(nameof(GetRolesByCountOfPermissions))]
        public ActionResult<PieChartViewModel> GetRolesByCountOfPermissions()
             => this.Ok(this.roleAnalysisService.GetRolesByCountOfPermissions());

        [HttpGet(nameof(GetRolesByUsersForMonths))]
        public ActionResult<PieChartViewModel> GetRolesByUsersForMonths([FromQuery] ChartFilterData filter)
           => this.Ok(this.roleAnalysisService.GetRolesByUsersForMonths(filter));

        [HttpGet(nameof(GetRolesByAverageCountOfUsersForWeek))]
        public ActionResult<PieChartViewModel> GetRolesByAverageCountOfUsersForWeek([FromQuery] ChartFilterData filter)
           => this.Ok(this.roleAnalysisService.GetRolesByAverageCountOfUsersForWeek(filter));

        [HttpGet(nameof(GetRolesByUsersLastWeek))]
        public ActionResult<PieChartViewModel> GetRolesByUsersLastWeek()
           => this.Ok(this.roleAnalysisService.GetRolesByUsersLastWeek());
    }
}
