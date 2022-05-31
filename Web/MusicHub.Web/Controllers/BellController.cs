namespace MusicHub.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.ViewModels.BellModels;

    using static MusicHub.Common.GlobalConstants;

    public class BellController : ApiController
    {
        private readonly IBellService bellService;

        public BellController(
            IBellService bellService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.bellService = bellService;
        }

        /// <summary>Get own Belling users.</summary>
        /// <returns>Ok(BellingPageModel<BellingViewModel>).</returns>
        [Authorize]
        [HttpGet(nameof(GetOwnBelling) + "/{page}")]
        public ActionResult<BellingPageModel<BellingViewModel>> GetOwnBelling(int page)
         => this.Ok(this.bellService.AllBellings<BellingViewModel>(page, PaginationData.BellsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Get Belling users.</summary>
        /// <param name="id">Id - user id.</param>
        /// <param name="page"></param>
        /// <returns>Ok(BellingPageModel<BellViewModel>).</returns>
        [HttpGet(nameof(GetBelling) + "/{id}/{page}")]
        public ActionResult<BellingPageModel<BellingViewModel>> GetBelling(string id, int page)
            => this.Ok(this.bellService.AllBellings<BellingViewModel>(page, PaginationData.BellsPerPage, id));

        /// <summary>Get bellers users.</summary>
        /// <param name="id">Id - user id.</param>
        /// <param name="page"></param>
        /// <returns>Ok(BellerPageModel<BellerViewModel>).</returns>
        [HttpGet(nameof(GetBellers) + "/{id}/{page}")]
        public ActionResult<BellerPageModel<BellerViewModel>> GetBellers(string id, int page)
            => this.Ok(this.bellService.AllBellers<BellerViewModel>(page, PaginationData.BellsPerPage, id));

        /// <summary>Create bell.</summary>
        /// <param name="model">BellCreateInputModel.
        /// <para>BellingId - Id of Belling user.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(BellCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            if (userId == model.BellingId)
            {
                return this.BadRequest();
            }

            if (this.bellService.IsBelling(userId, model.BellingId))
            {
                return this.BadRequest();
            }

            await this.bellService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Delete</summary>
        /// <param name="id">Id- Id of bell.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.bellService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.bellService.Delete(id);
            return this.Ok();
        }

        [HttpGet(nameof(FilterBellers) + "/{id}/{page}")]
        [Authorize]
        public ActionResult<BellerPageModel<BellerViewModel>> FilterBellers(string id, int page, [FromQuery] BellFilterInputModel model)
            => this.Ok(this.bellService.FilterBellers<BellerViewModel>(page, PaginationData.BellsPerPage, model, id));

        [HttpGet(nameof(FilterBellings) + "/{id}/{page}")]
        [Authorize]
        public ActionResult<BellingPageModel<BellingViewModel>> FilterBellings(string id, int page, [FromQuery] BellFilterInputModel model)
            => this.Ok(this.bellService.FilterBellings<BellingViewModel>(page, PaginationData.BellsPerPage, model, id));

        /// <summary>Check is bell user.</summary>
        /// <param name="id">Id- bell user Id.</param>
        /// <returns>Ok.</returns>
        [HttpGet(nameof(IsBelling) + "/{id}")]
        [Authorize]
        public ActionResult<bool> IsBelling(string id)
            => this.Ok(this.bellService.IsBelling(this.userManager.GetUserId(this.User), id));

        /// <summary>Get belling data.</summary>
        /// <param name="id">Id- beller Id.</param>
        /// <returns>Ok<string></returns>
        [Authorize]
        [HttpGet(nameof(GetBellingId) + "/{id}")]
        public ActionResult<string> GetBellingId(string id)
        {
            if (!this.bellService.IsBelling(this.userManager.GetUserId(this.User), id))
            {
                return this.Ok(new { Id = string.Empty });
            }

            var result = this.bellService.GetBell<BellerViewModel>(this.userManager.GetUserId(this.User), id).Id;
            return this.Ok(new { Id = result });
        }
    }
}
