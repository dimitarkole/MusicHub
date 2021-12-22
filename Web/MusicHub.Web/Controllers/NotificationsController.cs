using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.Infrastucture.Extensions;
using MusicHub.Web.ViewModels.NotificationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Controllers
{
    public class NotificationsController : ApiController
    {
        private readonly INotificationsService notificationsService;

        public NotificationsController(
            INotificationsService notificationsService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.notificationsService = notificationsService;
        }

        [Authorize]
        [HttpGet(nameof(GetAll) + "/{page}")]
        public ActionResult<NotificationAllViewModel<NotificationViewModel>> GetAll(int page)
        {
            var entitesPerPage = PaginationData.NotificationPerPage;
            return this.Ok(this.notificationsService.GetUserNotifications<NotificationViewModel>(page, entitesPerPage, this.User.GetUserId()));
        }

        [Authorize]
        [HttpPut("[action]/{notificationId}")]
        public async Task<ActionResult> MarkNotificationAsSeen(string notificationId)
        {
            if (this.User.GetUserId() != this.notificationsService.GetNotificationOwner(notificationId))
            {
                return this.Forbid();
            }

            await this.notificationsService.MarkAsSeen(notificationId);
            return this.Ok();
        }
    }
}
