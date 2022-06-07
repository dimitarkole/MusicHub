namespace MusicHub.Web.Controllers.AnalysisControlles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Analysis.Interfaces;
    using MusicHub.Web.ViewModels.ChartModels;
    using MusicHub.Web.ViewModels.MusicModels;
    using static MusicHub.Common.AnalysisConstants;

    public class OwnMusicAnalysisController : ApiController
    {
        private readonly IOwnMusicAnalysisService ownMusicAnalysis;

        public OwnMusicAnalysisController(
            IOwnMusicAnalysisService ownMusicAnalysis,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.ownMusicAnalysis = ownMusicAnalysis;
        }

        [HttpGet(nameof(GetCreatedMusicByCategoriesCountsAllTime))]
        public ActionResult<PieChartViewModel> GetCreatedMusicByCategoriesCountsAllTime()
             => this.Ok(this.ownMusicAnalysis.GetCreatedMusicByCategoriesCountsAllTime(this.userManager.GetUserId(this.User)));

        [HttpGet(nameof(GetTopCreatedMusicByCategoriesCountForMonths))]
        public ActionResult<LineChartViewModel> GetTopCreatedMusicByCategoriesCountForMonths([FromQuery] ChartFilterData filter)
            => this.Ok(this.ownMusicAnalysis.GetTopCreatedMusicByCategoriesCountForMonths(this.userManager.GetUserId(this.User), filter));

        [HttpGet(nameof(GetTopPlayedMusicsAllTime) + "/{page}")]
        public ActionResult<MusicPageModel<TopPlayedMusicViewModel>> GetTopPlayedMusicsAllTime(int page)
            => this.Ok(this.ownMusicAnalysis.GetTopPlayedMusicsAllTime<TopPlayedMusicViewModel>(page, OwnMusicAnalysis.PlayedMusicsCount, this.userManager.GetUserId(this.User)));
    }
}
