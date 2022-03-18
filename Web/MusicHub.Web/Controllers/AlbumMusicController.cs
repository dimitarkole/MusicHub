using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.Infrastucture.Extensions;
using MusicHub.Web.ViewModels.AlbumMusicModels;
using System.Threading.Tasks;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Controllers
{
    public class AlbumMusicController : ApiController
    {
        private readonly IAlbumService albumService;
        private readonly IAlbumMusicService albumMusicServices;
        private readonly IMusicService musicService;

        public AlbumMusicController(
            IAlbumService albumService,
            IAlbumMusicService albumMusicServices,
            IMusicService musicService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.albumService = albumService;
            this.albumMusicServices = albumMusicServices;
            this.musicService = musicService;
        }

        /// <summary>Add Music to album in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>Album - album id.</para>
        /// <para>MusicId - Music id.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost(nameof(Create))]
        [Authorize]
        public async Task<IActionResult> Create(AlbumMusicCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            if (!this.musicService.IsOwned(model.MusicId, userId))
            {
                return this.BadRequest("This music is not owned!");
            }

            await this.albumMusicServices.Create(model);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Get musics in albums.</summary>
        /// <param name="id">Id - album id.</param>
        /// <param name="page">The page of album Music.</param>
        /// <returns>Ok(AlbumMusicsPageModel<AlbumMusicViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{id}/{page}")]
        public ActionResult<AlbumMusicsPageModel<AlbumMusicViewModel>> Get(string id, int page)
            => this.Ok(this.albumMusicServices.All<AlbumMusicViewModel>(page, PaginationData.AlbumMusicsPerPage, id));

        /// <summary>Delete album music in data.</summary>
        /// <param name="id">Id- Id of album Music.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpDelete(nameof(Delete) + "/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var playlistId = this.albumMusicServices.GetAlbumIdByAlbumMusicId(id);
            if (!this.albumService.IsOwned(playlistId, userId))
            {
                return this.Forbid();
            }

            await this.albumMusicServices.Delete(id);
            return this.Ok();
        }
    }
}
