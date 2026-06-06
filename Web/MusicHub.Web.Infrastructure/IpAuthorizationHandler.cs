using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MusicHub.Data.Models;
using MusicHub.Services.Data;
using MusicHub.Services.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Web.Infrastructure
{
    public class IpAuthorizationHandler : AuthorizationHandler<IpRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly UserIpRuleService userIpRuleService;

        public IpAuthorizationHandler(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            UserIpRuleService userIpRuleService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.userIpRuleService = userIpRuleService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IpRequirement requirement)
        {
            var httpContext = this.httpContextAccessor.HttpContext;
            var ip = httpContext.Connection.RemoteIpAddress?.ToString();
            var userId = this.userManager.GetUserId(httpContext.User);
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(ip))
            {
                return Task.CompletedTask;
            }

            var hasAccess = this.userIpRuleService.HasAccess(userId, ip);
            if (hasAccess)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
