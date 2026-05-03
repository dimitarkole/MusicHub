using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.ViewModels.MusicModels;
using MusicHub.Web.ViewModels.UserLoginHistoryModels;
using System.Collections.Generic;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Controllers
{
    public class UserLoginHistoryController : ApiController
    {
        private readonly IUserLoginHistoryService userLoginHistoryService;

        public UserLoginHistoryController(
            IUserLoginHistoryService userLoginHistoryService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.userLoginHistoryService = userLoginHistoryService;
        }

        /// <summary>Filter user login history.</summary>
        /// <param name="page">The page of user login history.</param>
        /// <param name="filter">Filter data.
        /// </param>
        /// <returns>Ok(UserLoginHistoryPageModel<UserLoginHistoryViewModel>).</returns>
        [HttpGet(nameof(Filter) + "/{page}")]
        public ActionResult<UserLoginHistoryPageModel<UserLoginHistoryViewModel>> Filter(int page, [FromQuery] UserLoginHistoryFilterInputModel filter)
        {
            var userId = this.userManager.GetUserId(this.User);
            return this.Ok(this.userLoginHistoryService.Filter<UserLoginHistoryViewModel>(page, PaginationData.UserLoginHistoryPerPage, userId, filter));
        }

        /// <summary>Get user login history. by id.</summary>
        /// <param name="id">Id of user login history..</param>
        /// <returns>Ok(MusicPlayModel).</returns>
        [HttpGet("{id}")]
        public ActionResult<UserLoginHistoryViewModel> Get(string id)
            => this.Ok(this.userLoginHistoryService.GetById<UserLoginHistoryViewModel>(id));
    }
}
