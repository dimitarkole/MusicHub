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
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.Infrastucture;
    using MusicHub.Web.Infrastucture.Extensions;
    using MusicHub.Web.ViewModels.ReportMusicModels;

    using static MusicHub.Common.GlobalConstants;

    public class ReportMusicController : ApiController
    {

        private readonly IMusicService musicService;
        private readonly IReportMusicService reportMusicService;

        public ReportMusicController(
            IMusicService musicService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger,
            IReportMusicService reportMusicService)
            : base(userManager, signInManager, logger)
        {
            this.musicService = musicService;
            this.reportMusicService = reportMusicService;
        }

        /// <summary>Get all reported Musics.</summary>
        /// <param name="page">The page of Musics.</param>
        /// <param name="filter">Filter data.
        /// <returns>Ok(ReportMusicPageModel<ReportMusicViewModel>).</returns>
        [HttpGet(nameof(GetAllReportedMusic) + "/{page}")]
        [Authorize]
        [Permission(PermissionType.ReportedMusicViewer)]
        public ActionResult<ReportMusicPageModel<ReportMusicViewModel>> GetAllReportedMusic(int page)
          => this.Ok(this.musicService.AllReportedMusic<ReportMusicViewModel>(page, PaginationData.ReportedMusicsPerPage));

        /// <summary>Filter reported Musics.</summary>
        /// <param name="page">The page of Musics.</param>
        /// <param name="filter">Filter data.
        /// <returns>Ok(ReportMusicPageModel<ReportMusicViewModel>).</returns>
        [HttpPost(nameof(FilterReportedMusic) + "/{page}")]
        [Authorize]
        [Permission(PermissionType.ReportedMusicViewer)]
        public ActionResult<ReportMusicPageModel<ReportMusicViewModel>> FilterReportedMusic(int page, ReportMusicFilter filter)
          => this.Ok(this.musicService.FilterReported<ReportMusicViewModel>(filter, page, PaginationData.ReportedMusicsPerPage));

        /// <summary>Get all reported.</summary>
        /// <param name="page">The page of reports.</param>
        /// <param name="filter">Filter data.
        /// <returns>Ok(ReportPageModel<ReportViewModel>).</returns>
        [HttpGet(nameof(GetAllReported) + "/{id}/{page}")]
        [Authorize]
        [Permission(PermissionType.ReportedMusicViewer)]
        public ActionResult<ReportPageModel<ReportViewModel>> GetAllReported(string id, int page)
          => this.Ok(this.reportMusicService.All<ReportViewModel>(id, page, PaginationData.ReportsPerPage));

        /// <summary>Filter reported reported.</summary>
        /// <param name="page">The page of reported.</param>
        /// <param name="filter">Filter data.
        /// <returns>Ok(ReportPageModel<ReportViewModel>).</returns>
        [HttpPost(nameof(FilterReported) + "/{id}/{page}")]
        [Authorize]
        [Permission(PermissionType.ReportedMusicViewer)]
        public ActionResult<ReportPageModel<ReportViewModel>> FilterReported(string id, int page, ReportFilter filter)
          => this.Ok(this.reportMusicService.Filter<ReportViewModel>(id, filter, page, PaginationData.ReportsPerPage));

        /// <summary>Add report music in database.</summary>
        /// <param name="model">Input data.</param>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(ReportMusicCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            var musicId = await this.reportMusicService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { reportMusicId = musicId });
        }

        /// <summary>Delete report Music from database.</summary>
        /// <param name="id">Id - Id of reported music.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [Permission(PermissionType.ReportedMusicViewer)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.reportMusicService.Delete(id);
            return this.Ok();
        }

        /// <summary>Get reported music by id.</summary>
        /// <param name="id">Id of music.</param>
        /// <returns>Ok(MusicPlayModel).</returns>
        [HttpGet("{id}")]
        public ActionResult<ReportDetatilsModel> Get(string id)
            => this.Ok(this.reportMusicService.GetById<ReportDetatilsModel>(id));

        /// <summary>Delete report Music from database.</summary>
        /// <param name="id">Id - Id of reported music.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [Permission(PermissionType.MusicCreator)]
        [HttpDelete(nameof(DeleteMusic) + "{id}")]
        public async Task<IActionResult> DeleteMusic(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.musicService.Delete(id);
            return this.Ok();
        }
    }
}
