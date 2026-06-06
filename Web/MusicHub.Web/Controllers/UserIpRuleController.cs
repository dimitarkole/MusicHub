namespace MusicHub.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.ViewModels.UserIpRulesModels;

    public class UserIpRuleController : ApiController
    {
        private readonly IUserIpRuleService userIpRuleService;

        public UserIpRuleController(
            IUserIpRuleService userIpRuleService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.userIpRuleService = userIpRuleService;
        }

        [HttpGet]
        public IActionResult Filter([FromQuery] UserIpRuleFilterInputModel model, int page = 1, int entitesPerPage = 10)
        {
            
            var userId = this.userManager.GetUserId(this.User);
            var result = this.userIpRuleService.Filter<UserIpRuleViewModel>(page, entitesPerPage, model, userId);
            return this.Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserIpRuleCreateInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            var id = await this.userIpRuleService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { userIpRuleId = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UserIpRuleEditInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            var isOwned = this.userIpRuleService.IsOwned(id, userId);
            if (!isOwned)
            {
                return this.Forbid();
            }

            await this.userIpRuleService.Update(model, id);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var isOwned = this.userIpRuleService.IsOwned(id, userId);
            if (!isOwned)
            {
                return this.Forbid();
            }

            await this.userIpRuleService.Delete(id);
            return this.Ok();
        }
    }
}
