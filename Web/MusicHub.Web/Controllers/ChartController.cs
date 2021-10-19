using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.ChartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHub.Web.Controllers
{
    public class ChartController : ApiController
    {
        public ChartController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger) 
            : base(userManager, signInManager, logger)
        {
        }

        [HttpGet(nameof(Get))]
        public ActionResult<LineChartViewModel> Get()
        {
            var resultData = new List<LineChartDataViewModel>() {
                new LineChartDataViewModel() {
                    Name = "simple",
                    Series = new List<LineChartSeriesViewModel> {
                        new LineChartSeriesViewModel() { Name = "Jan", Value = 5 },
                        new LineChartSeriesViewModel() { Name = "Fev", Value = 15 },
                        new LineChartSeriesViewModel() { Name = "Mart", Value = 2 },
                    },
                },
                new LineChartDataViewModel() {
                    Name = "simple2",
                    Series = new List<LineChartSeriesViewModel> {
                        new LineChartSeriesViewModel() { Name = "Jan", Value = 15 },
                        new LineChartSeriesViewModel() { Name = "Fev", Value = 12 },
                        new LineChartSeriesViewModel() { Name = "Mart", Value = 2},
                    },
                },
                new LineChartDataViewModel() {
                    Name = "simple3",
                    Series = new List<LineChartSeriesViewModel> {
                        new LineChartSeriesViewModel() { Name = "Jan", Value = 185 },
                        new LineChartSeriesViewModel() { Name = "Fev", Value = 6 },
                        new LineChartSeriesViewModel() { Name = "Mart", Value = 51 },
                    },
                },
            };
            var result = new LineChartViewModel()
            {
                Name = "Simple",
                XAxisLabel = "XAxisLabel",
                YAxisLabel = "YAxisLabel",
                Data = resultData,
            };

            return this.Ok(result);

        }
    }
}
