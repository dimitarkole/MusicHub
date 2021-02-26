namespace MusicHub.Web.Controllers
{
    using System.Diagnostics;

    using MusicHub.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using MusicHub.Data.Models;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

    public class HomeController : ApiController
    {
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LogoutModel> logger) : base(userManager, signInManager, logger)
        {
        }
    }
}
