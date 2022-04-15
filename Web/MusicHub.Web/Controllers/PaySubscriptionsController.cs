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
    using MusicHub.Web.ViewModels.PaySubscriptionModels;

    using static MusicHub.Common.GlobalConstants;

    [Authorize]
    public class PaySubscriptionsController : ApiController
    {
        private readonly IPaySubscriptionService paySubscriptionService;

        public PaySubscriptionsController(
            IPaySubscriptionService paySubscriptionService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.paySubscriptionService = paySubscriptionService;
        }

        /// <summary>Get own PaySubscription.</summary>
        /// <param name="page">The page of PaySubscription.</param>
        /// <returns>Ok(PaySubscriptionPageModel<PaySubscriptionViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{page}")]
        [Permission(PermissionType.UserAdministration)]
        public ActionResult<PaySubscriptionPageModel<PaySubscriptionViewModel>> Get(int page)
            => this.Ok(this.paySubscriptionService.All<PaySubscriptionViewModel>(page, PaginationData.PaySubscriptionsPerPage));

        /// <summary>Get by id PaySubscription.</summary>
        /// <param name="id">The page of PaySubscription.</param>
        /// <returns>Ok(PaySubscriptionDetailsModel).</returns>
        [HttpGet(nameof(GetById) + "/{id}")]
        [Permission(PermissionType.UserAdministration)]
        public ActionResult<PaySubscriptionDetailsModel> GetById(string id)
            => this.Ok(this.paySubscriptionService.GetById<PaySubscriptionDetailsModel>(id));

        /// <summary>Get own PaySubscription.</summary>
        /// <param name="page">The page of PaySubscription.</param>
        /// <returns>Ok(PaySubscriptionPageModel<PaySubscriptionViewModel>).</returns>
        [HttpGet(nameof(Filter) + "/{page}")]
        [Permission(PermissionType.UserAdministration)]
        public ActionResult<PaySubscriptionPageModel<PaySubscriptionViewModel>> Filter(int page, PaySubscriptionFilter filter)
            => this.Ok(this.paySubscriptionService.Filter<PaySubscriptionViewModel>(page, PaginationData.PaySubscriptionsPerPage, filter));

        /// <summary>Get own PaySubscription.</summary>
        /// <param name="page">The page of PaySubscription.</param>
        /// <returns>Ok(PaySubscriptionPageModel<PaySubscriptionViewModel>).</returns>
        [HttpGet(nameof(GetOwn) + "/{page}")]
        public ActionResult<PaySubscriptionPageModel<PaySubscriptionViewModel>> GetOwn(int page)
            => this.Ok(this.paySubscriptionService.All<PaySubscriptionViewModel>(page, PaginationData.PaySubscriptionsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Filter own PaySubscription.</summary>
        /// <returns>Ok(PaySubscriptionPageModel<PaySubscriptionViewModel>).</returns>
        [HttpPost(nameof(FilterOwn) + "/{page}")]
        public ActionResult<PaySubscriptionPageModel<PaySubscriptionViewModel>> FilterOwn(int page, PaySubscriptionFilter filter)
          => this.Ok(this.paySubscriptionService.Filter<PaySubscriptionViewModel>(page, PaginationData.PaySubscriptionsPerPage, filter, this.userManager.GetUserId(this.User)));

        /// <summary>Add Music in data.</summary>
        /// <param name="model">Input data.</param>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(PaySubscriptionCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            var paySubscriptionId = await this.paySubscriptionService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { paySubscriptionId = paySubscriptionId });
        }

        /// <summary>Edit PaySubscription data.</summary>
        /// <returns>Ok().</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, PaySubscriptionEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.paySubscriptionService.Update(id, model);
            if (!this.paySubscriptionService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            return this.Ok();
        }
    }
}
