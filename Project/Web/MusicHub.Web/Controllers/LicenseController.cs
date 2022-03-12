namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.LicenseModels;

    using static MusicHub.Common.GlobalConstants;

    public class LicenseController : ApiController
    {
        private readonly ILicenseService licenseService;

        public LicenseController(
            ILicenseService licenseService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.licenseService = licenseService;
        }

        /// <summary>Get all licenes.</summary>
        /// <param name="page">The page of license.</param>
        /// <returns>Ok(LicenseAllViewModel<LicenceViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{page}")]
        [Authorize(Roles = Roles.AdministratorRoleName)]
        public ActionResult<LicenseAllViewModel<LicenseLargeViewModel>> Get(int page)
            => this.Ok(this.licenseService.All<LicenseLargeViewModel>(page, PaginationData.LicensesPerPage));

        /// <summary>Get own licenses.</summary>
        /// <param name="page">The page of licenses.</param>
        /// <returns>Ok(LicenseAllViewModel<LicenceViewModel>).</returns>
        [HttpGet(nameof(GetOwn) + "/{page}")]
        [Authorize(Roles = Roles.UserRoleName)]
        public ActionResult<LicenseAllViewModel<LicenceViewModel>> GetOwn(int page)
            => this.Ok(this.licenseService.AllOwn<LicenceViewModel>(page, PaginationData.LicensesPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Get own licenses.</summary>
        /// <returns>Ok(LicenseAllViewModel. <LicenceSmallViewModel>).</returns>
        [HttpGet(nameof(GetOwnApproved))]
        [Authorize(Roles = Roles.UserRoleName)]
        public ActionResult<LicenseAllViewModel<LicenceSmallViewModel>> GetOwnApproved()
            => this.Ok(this.licenseService.AllOwnApproved<LicenceSmallViewModel>(this.userManager.GetUserId(this.User)));

        /// <summary>Get own licenses.</summary>
        /// <param name="page">The page of licenses.</param>
        /// <returns>Ok(LicenseAllViewModel.<LicenceSmallViewModel>).</returns>
        [HttpPost(nameof(Filter) + "/{page}")]
        [Authorize(Roles = Roles.AdministratorRoleName)]
        public ActionResult<LicenseAllViewModel<LicenseLargeViewModel>> Filter(int page, LicenseFilter licenseFilter)
            => this.Ok(this.licenseService.Filter<LicenseLargeViewModel>(page, PaginationData.LicensesPerPage, licenseFilter));

        /// <summary>Get own licenses.</summary>
        /// <param name="page">The page of licenses.</param>
        /// <returns>Ok(LicenseAllViewModel.<LicenceSmallViewModel>).</returns>
        [HttpPost(nameof(FilterOwn) + "/{page}")]
        [Authorize(Roles = Roles.UserRoleName)]
        public ActionResult<LicenseAllViewModel<LicenceSmallViewModel>> FilterOwn(int page, LicenseFilter licenseFilter)
            => this.Ok(this.licenseService.Filter<LicenceSmallViewModel>(page, PaginationData.LicensesPerPage, licenseFilter, this.userManager.GetUserId(this.User)));

        /// <summary>Get music by id.</summary>
        /// <param name="id">Id of music.</param>
        /// <returns>Ok(LicencesDetailModel).</returns>
        [HttpGet(nameof(GetById) + "/{id}")]
        public ActionResult<LicenseLargeViewModel> GetById(string id)
            => this.Ok(this.licenseService.GetById<LicenseLargeViewModel>(id));

        /// <summary>Add license in data.</summary>
        /// <param name="model">Input data.</param>
        /// <returns>Status201Created.</returns>
        [HttpPost(nameof(Post))]
        //[Authorize(Roles = Roles.UserRoleName)]
        public async Task<IActionResult> Post(LicenseCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            var licenseId = await this.licenseService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, licenseId);
        }

        /// <summary>Edit license data.</summary>
        /// <param name="id">Id - Id of license.</param>
        /// <param name="model">Input data.</param>
        /// <returns>Ok().</returns>
        [Authorize(Roles = Roles.UserRoleName)]
        [HttpPut(nameof(Put) + "/{id}")]
        public async Task<IActionResult> Put(string id, LicenseEditInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (this.licenseService.IsOwn(id, userId))
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable);
            }

            await this.licenseService.Update(id, model);
            return this.Ok();
        }

        /// <summary>Change license status data.</summary>
        /// <param name="id">Id - Id of license.</param>
        /// <param name="model">Input data.</param>
        /// <returns>Ok().</returns>
        [Authorize(Roles = Roles.AdministratorRoleName)]
        [HttpPut(nameof(ChangeStatus) + "/{id}")]
        public async Task<IActionResult> ChangeStatus(string id, LicenseStatusInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.licenseService.ChangeStatus(id, model);
            return this.Ok();
        }

        /// <summary>Delete license in data.</summary>
        /// <param name="id">Id - Id of license.</param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = Roles.UserRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!this.licenseService.IsOwn(id, this.userManager.GetUserId(this.User)))
            {
                return this.Forbid();
            }

            await this.licenseService.Delete(id);
            return this.Ok();
        }

        /// <summary>Add license file in data.</summary>
        /// <param name="model">Input data.</param>
        /// <returns>Status201Created.</returns>
        [HttpPost(nameof(PostFiles))]
        [Authorize(Roles = Roles.UserRoleName)]
        public async Task<IActionResult> PostFiles(LicenseFileCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.licenseService.CreateFile(model);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Delete license file in data.</summary>
        /// <param name="id">Id - Id of license file.</param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = Roles.UserRoleName)]
        [HttpDelete(nameof(DeleteFile) + "/{id}")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            await this.licenseService.DeleteFile(id);
            return this.Ok();
        }
    }
}
