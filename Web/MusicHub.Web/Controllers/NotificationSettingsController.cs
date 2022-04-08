namespace MusicHub.Web.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.Infrastucture.Extensions;
    using MusicHub.Web.ViewModels.NotificationSetingModels;

    public class NotificationSettingsController : ApiController
    {
        private readonly INotificationSettingsService notificationSettingsService;

        public NotificationSettingsController(
            INotificationSettingsService notificationSettingsService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.notificationSettingsService = notificationSettingsService;
        }

        /// <summary>Get user's notification settings.</summary>
        /// <returns>Ok(IEnumerable<NotificationSettingsViewModel>).</returns>
        [Authorize]
        [HttpGet(nameof(Get))]
        public ActionResult<IEnumerable<NotificationSettingsViewModel>> Get()
          => this.Ok(this.notificationSettingsService.All<NotificationSettingsViewModel>(this.User.GetUserId()));

        /// <summary>Get user's notification setting by type.</summary>
        /// <returns>Ok(NotificationSettingsViewModel).</returns>
        [Authorize]
        [HttpPost(nameof(GetByType))]
        public ActionResult<NotificationSettingsViewModel> GetByType(NotificationSettingsTypeInputModel model)
          => this.Ok(this.notificationSettingsService.GetByType<NotificationSettingsViewModel>(model.Type, this.User.GetUserId()));

        /// <summary>Get user's notification settings by type.</summary>
        /// <returns>Ok(bool).</returns>
        [Authorize]
        [HttpPost(nameof(IsEnabled))]
        public ActionResult<bool> IsEnabled(NotificationSettingsTypeInputModel model)
          => this.Ok(this.notificationSettingsService.IsEnabled(model.Type, this.User.GetUserId()));

        /// <summary>Add notification setting in data.</summary>
        /// <param name="model">Input data.</param>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(NotificationSettingCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            var notificationSettingId = await this.notificationSettingsService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { notificationSettingId = notificationSettingId });
        }

        /// <summary>Edit notification settings data.</summary>
        /// <param name="id">Id - Id of notification settings.</param>
        /// <param name="model">Input data.</param>
        /// <returns>Ok().</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, NotificationSettingEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            if (!this.notificationSettingsService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.notificationSettingsService.Update(id, model);
            return this.Ok();
        }

        /// <summary>Delete notification settings in data.</summary>
        /// <param name="id">Id - Id of notification settings.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.notificationSettingsService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.notificationSettingsService.Delete(id);
            return this.Ok();
        }
    }
}
