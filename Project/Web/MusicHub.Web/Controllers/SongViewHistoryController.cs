namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.SongViewHistoryModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using static MusicHub.Common.GlobalConstants;

    public class SongViewHistoryController : UserController
    {
        private readonly ISongViewHistoryService songViewHistoryService;

        public SongViewHistoryController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger,
            ISongViewHistoryService songViewHistoryService)
            : base(userManager, signInManager, logger)
        {
            this.songViewHistoryService = songViewHistoryService;
        }

        /// <summary>Get own history.</summary>
        /// <param name="page">The page of songs.</param>
        /// <returns>Ok(SongViewHistoryAllViewModel<SongViewHistoryViewModelModels>).</returns>
        [HttpGet("{page}")]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        public ActionResult<SongViewHistoryAllViewModel<SongViewHistoryViewModelModels>> Get(int page)
            => this.Ok(this.songViewHistoryService
                .All<SongViewHistoryViewModelModels>(page, PaginationData.SongsViewHistoryPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Filter own history.</summary>
        /// <param name="page">The page of songs.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of song.</para>
        /// <para>Username - name of user who created song.</para>
        /// <para>UserId - id of user who created song.</para>
        /// <para>CategoryId - id of category of song.</para>
        /// <para>OrderMethod - order method.</para>
        /// <returns>Ok(SongViewHistoryAllViewModel<SongViewHistoryViewModelModels>).</returns>
        [HttpGet(nameof(Filter) + "/{page}")]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        public ActionResult<SongViewHistoryAllViewModel<SongViewHistoryViewModelModels>> Filter(int page, SongViewHistoryFilterInputModels filter)
            => this.Ok(this.songViewHistoryService
                .Search<SongViewHistoryViewModelModels>(page, PaginationData.SongsViewHistoryPerPage, filter, this.userManager.GetUserId(this.User)));

        /// <summary>Add history in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>SongId - id of song.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        public async Task<IActionResult> Post(SongViewHistoryCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.songViewHistoryService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Delete history in data.</summary>
        /// <param name="id">Id - Id of history.</param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.songViewHistoryService.Delete(id);
            return this.Ok();
        }
    }
}
