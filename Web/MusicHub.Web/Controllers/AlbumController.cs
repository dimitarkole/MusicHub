namespace MusicHub.Web.Controllers
{
    using System.Collections.Generic;
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
    using MusicHub.Web.ViewModels.AlbumModels;

    using static MusicHub.Common.GlobalConstants;

    public class AlbumController : ApiController
    {
        private readonly IAlbumService albumService;

        public AlbumController(
            IAlbumService albumService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.albumService = albumService;
        }

        /// <summary>Get owned albums.</summary>
        /// <param name="page">The page of albums.</param>
        /// <returns>Ok(AlbumPageModel<AlbumViewModel>).</returns>
        [HttpGet(nameof(GetOwn) + "/{page}")]
        [Authorize]
        // [Permission(PermissionType.AlbumCreator)]
        public ActionResult<AlbumPageModel<AlbumViewModel>> GetOwn(int page)
           => this.Ok(this.albumService.All<AlbumViewModel>(page, PaginationData.AlbumsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Is own album.</summary>
        /// <returns>true/false.</returns>
        [HttpGet(nameof(IsOwned) + "/{id}")]
        [Authorize]
        [Permission(PermissionType.AlbumCreator)]
        public IActionResult IsOwned(string id)
             => this.Ok(this.albumService.IsOwned(id, this.userManager.GetUserId(this.User)));

        /// <summary>Get own albums where Music is not added.</summary>
        /// <param name="id">Id - Music id</param>
        /// <returns>Ok(IEnumerable<AlbumViewModel>).</returns>
        [HttpGet(nameof(GetOwnForAddingMusic) + "/{id}")]
        [Authorize]
        [Permission(PermissionType.AlbumCreator)]
        public ActionResult<IEnumerable<AlbumViewModel>> GetOwnForAddingMusic(string id)
           => this.Ok(this.albumService.GetOwnedForAddingMusic<AlbumViewModel>(id, this.userManager.GetUserId(this.User)));

        /// <summary>Add album in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>Name - album name.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize]
        [Permission(PermissionType.AlbumCreator)]
        public async Task<IActionResult> Post(AlbumCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            var albumId = await this.albumService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { albumId = albumId });
        }

        /// <summary>Edit alnum.</summary>
        /// <param name="id">Id- Id of alnum.</param>
        /// <param name="model">CommentEditInputModel - new alnum data.
        /// <para>Name - alnum name.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [HttpPut("{id}")]
        [Authorize]
        [Permission(PermissionType.AlbumCreator)]
        public async Task<IActionResult> Put(string id, AlbumEditInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            if (!this.albumService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.albumService.Update(model, id);
            return this.Ok();
        }

        /// <summary>Delete alnum.</summary>
        /// <param name="id">Id- Id of alnum.</param>
        /// <returns>Ok.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [Permission(PermissionType.AlbumCreator)]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.albumService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.albumService.Delete(id);
            return this.Ok();
        }

        /// <summary>Filter own alnum.</summary>
        /// <param name="page">The page of alnum.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of alnum.</para>
        /// <para>Username - name of user who created alnum.</para>
        /// <para>UserId - id of user who created alnum.</para>
        /// <para>CategoryId - id of category of alnum.</para>
        /// <para>OrderMethod - order method.</para>
        /// <returns>Ok(AlbumPageModel<AlbumViewModel>).</returns>
        [HttpGet(nameof(FilterOwn) + "/{page}")]
        [Authorize]
        [Permission(PermissionType.AlbumCreator)]
        public ActionResult<AlbumPageModel<AlbumViewModel>> FilterOwn(int page, [FromQuery] AlbumFilterInputModel filter)
            => this.Ok(this.albumService.Filter<AlbumViewModel>(page, PaginationData.AlbumsPerPage, filter, this.userManager.GetUserId(this.User)));

        /// <summary>Get albums.</summary>
        /// <param name="page">The page of album.</param>
        /// <returns>Ok(AlbumPageModel<AlbumViewModel>).</returns>
        [HttpGet(nameof(GetAlbums) + "/{page}")]
        public ActionResult<AlbumPageModel<AlbumViewModel>> GetAlbums(int page)
            => this.Ok(this.albumService.All<AlbumViewModel>(page, PaginationData.AlbumsPerPage));

        /// <summary>Get album by id.</summary>
        /// <param name="id">The id of palylists.</param>
        /// <returns>Ok(AlbumPlayModel).</returns>
        [HttpGet(nameof(GetAlbum) + "/{id}")]
        public ActionResult<AlbumPlayModel> GetAlbum(string id)
           => this.Ok(this.albumService.GetById<AlbumPlayModel>(id));

        /// <summary>Filter albums.</summary>
        /// <param name="page">The page of music.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of album.</para>
        /// <para>Username - name of user who created album.</para>
        /// <para>UserId - id of user who created album.</para>
        /// <para>CategoryId - id of category of album.</para>
        /// <para>OrderMethod - order method.</para>
        /// </param>
        /// <returns>Ok(AlbumPageModel<AlbumViewModel>).</returns>
        [HttpGet(nameof(AlbumsFilter) + "/{page}")]
        public ActionResult<AlbumPageModel<AlbumViewModel>> AlbumsFilter(int page, [FromQuery] AlbumFilterInputModel model)
            => this.Ok(this.albumService.Filter<AlbumViewModel>(page, PaginationData.AlbumsPerPage, model));
    }
}
