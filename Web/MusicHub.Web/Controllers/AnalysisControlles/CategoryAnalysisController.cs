namespace MusicHub.Web.Controllers.AnalysisControlles
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
    using MusicHub.Services.Data.Analysis.Interfaces;
    using MusicHub.Web.Infrastucture;
    using MusicHub.Web.ViewModels.ChartModels;
    using static MusicHub.Common.AnalysisConstants;

    [Authorize]
    [Permission(PermissionType.CategoryAdministrator)]
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

        [HttpGet(nameof(GetCategoriesWithMusicsCount))]
        public ActionResult<PieChartViewModel> GetCategoriesWithMusicsCount([FromQuery] ChartFilterData filter)
             => this.Ok(this.categoryAnalysisService.GetCategoriesWithMusicsCount(filter));

        [HttpGet(nameof(GetTopCategoriesByUploadMusicsCountForMonths))]
        public ActionResult<LineChartViewModel> GetTopCategoriesByUploadMusicsCountForMonths([FromQuery] ChartFilterData filter)
            => this.Ok(this.categoryAnalysisService.GetTopCategoriesByUploadMusicsCountForMonths(filter));

        [HttpGet(nameof(GetTopCategoriesByPlayedMusicsCountForMonths))]
        public ActionResult<LineChartViewModel> GetTopCategoriesByPlayedMusicsCountForMonths([FromQuery] ChartFilterData filter)
           => this.Ok(this.categoryAnalysisService.GetTopCategoriesByPlayedMusicsCountForMonths(filter));

        [HttpGet(nameof(GetCreatedCategoriesCountsByMonths))]
        public ActionResult<PieChartViewModel> GetCreatedCategoriesCountsByMonths([FromQuery] ChartFilterData filter)
           => this.Ok(this.categoryAnalysisService.GetCreatedCategoriesCountsByMonths(filter));

        [HttpGet(nameof(GetCategoriesByPlayedMusicsCountAllTime))]
        public ActionResult<PieChartViewModel> GetCategoriesByPlayedMusicsCountAllTime([FromQuery] ChartFilterData filter)
         => this.Ok(this.categoryAnalysisService.GetCategoriesByPlayedMusicsCountAllTime(filter));
    }
}
