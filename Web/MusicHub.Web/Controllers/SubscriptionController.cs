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
    using MusicHub.Web.ViewModels.SubscriptionModels;

    using static MusicHub.Common.GlobalConstants;

    public class SubscriptionController : ApiController
    {
        private readonly ISubscriptionService subscriptionService;

        public SubscriptionController(
            ISubscriptionService subscriptionService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.subscriptionService = subscriptionService;
        }

        [HttpGet(nameof(Get) + "/{page}")]
        public ActionResult<SubscriptionsAllViewModel<SubscriptionViewModel>> Get(int page)
         => this.Ok(this.subscriptionService.All<SubscriptionViewModel>(page, PaginationData.SubscriptionsPerPage));

        [HttpPost(nameof(Filter) + "/{page}")]
        public ActionResult<SubscriptionsAllViewModel<SubscriptionViewModel>> Filter(int page, SubscriptionFilter model)
          => this.Ok(this.subscriptionService.Filter<SubscriptionViewModel>(page, PaginationData.SubscriptionsPerPage, model));

        [HttpPost]
        [Authorize]
        [Permission(PermissionType.SubscriptionManager)]
        public async Task<IActionResult> Post(SubscriptionCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.subscriptionService.Create(model);
            return this.Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        [Permission(PermissionType.SubscriptionManager)]
        public async Task<IActionResult> Put(string id, SubscriptionEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.subscriptionService.Update(id, model);
            return this.Ok();
        }

        [Authorize]
        [Permission(PermissionType.SubscriptionManager)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.subscriptionService.Delete(id);
            return this.Ok();
        }
    }
}
