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
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Analysis.Interfaces;
    using MusicHub.Web.ViewModels.ChartModels;
    using static MusicHub.Common.AnalysisConstants;

    public class CategoryAnalysisController : ApiController
    {
        private readonly ICategoryAnalysisService categoryAnalysisService;

        public CategoryAnalysisController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger,
            ICategoryAnalysisService categoryAnalysisService)
            : base(userManager, signInManager, logger)
        {
            this.categoryAnalysisService = categoryAnalysisService;
        }

        [HttpGet(nameof(GetCategoriesWithMusicsAllTime))]
        public ActionResult<PieChartViewModel> GetCategoriesWithMusicsAllTime()
             => this.Ok(this.categoryAnalysisService.GetCategoriesWithMusicsAllTime());

        [HttpPost(nameof(GetCategoriesWithMusicsCount))]
        public ActionResult<PieChartViewModel> GetCategoriesWithMusicsCount(ChartFilterData filter)
             => this.Ok(this.categoryAnalysisService.GetCategoriesWithMusicsCount(filter));

        [HttpPost(nameof(GetTopCategoriesByUploadMusicsCountForMonths))]
        public ActionResult<LineChartViewModel> GetTopCategoriesByUploadMusicsCountForMonths(ChartFilterData filter)
            => this.Ok(this.categoryAnalysisService.GetTopCategoriesByUploadMusicsCountForMonths(filter));

        [HttpPost(nameof(GetTopCategoriesByPlayedMusicsCountForMonths))]
        public ActionResult<LineChartViewModel> GetTopCategoriesByPlayedMusicsCountForMonths(ChartFilterData filter)
           => this.Ok(this.categoryAnalysisService.GetTopCategoriesByPlayedMusicsCountForMonths(filter));

        [HttpPost(nameof(GetCreatedCategoriesCountsByMonths))]
        public ActionResult<PieChartViewModel> GetCreatedCategoriesCountsByMonths(ChartFilterData filter)
           => this.Ok(this.categoryAnalysisService.GetCreatedCategoriesCountsByMonths(filter));

        [HttpPost(nameof(GetCategoriesByPlayedMusicsCountAllTime))]
        public ActionResult<PieChartViewModel> GetCategoriesByPlayedMusicsCountAllTime(ChartFilterData filter)
         => this.Ok(this.categoryAnalysisService.GetCategoriesByPlayedMusicsCountAllTime(filter));
    }
}
