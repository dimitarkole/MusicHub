namespace MusicHub.Services.Data.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Services.Data.Analysis.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.ChartModels;
    using MusicHub.Web.ViewModels.MusicModels;
    using static MusicHub.Common.AnalysisConstants;

    public class OwnMusicAnalysisService : IOwnMusicAnalysisService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public OwnMusicAnalysisService(ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public PieChartViewModel GetCreatedMusicByCategoriesCountsAllTime(string userId)
            => new PieChartViewModel()
            {
                Name = OwnMusicAnalysis.GetCreatedMusicByCategoriesCountsAllTime,
                Series = this.context.Musics
                                .Where(m => m.UserId == userId)
                                .GroupBy(m => m.Category.Name)
                                .Select(m => new PieChartSeriesViewModel()
                                {
                                    Name = m.Key,
                                    Value = m.Count(),
                                })
                                .ToList(),
            };

        public LineChartViewModel GetTopCreatedMusicByCategoriesCountForMonths(string userId, ChartFilterData filter)
        {
            var today = DateTime.UtcNow;
            var treeMonthsBefore = today.AddMonths(-filter.CountOfMonthsBefore);

            var categories = this.context.Categories
                .Include(c => c.Musics)
                .OrderBy(c => c.Name);
            var chartData = new List<LineChartDataViewModel>();
            foreach (var category in categories)
            {
                var series = this.context.Musics
                        .Where(m => m.UserId == userId
                                && m.CategoryId == category.Id
                                 && m.CreatedOn > treeMonthsBefore
                                 && m.CreatedOn < today)
                        .GroupBy(g => new { Month = g.CreatedOn.Month, Year = g.CreatedOn.Year })
                        .Select(r => new LineChartSeriesViewModel()
                        {
                            Name = r.Key.Month + "/" + r.Key.Year,
                            Value = r.Count(),
                        })
                        .ToList();

                var data = new LineChartDataViewModel()
                {
                    Name = category.Name,
                    Series = series,
                };

                chartData.Add(data);
            }

            return new LineChartViewModel()
            {
                Data = chartData
                    .OrderByDescending(d => d.Series.Sum(s => s.Value))
                    .Take(filter.CountOfSeries)
                    .ToList(),
                Name = CategoryAnalysis.GetTopCategoriesByPlayedMusicsCountForMonths,
                XAxisLabel = CategoryAnalysis.MonthsXLabel,
                YAxisLabel = CategoryAnalysis.MusicYLabel,
            };
        }

        public MusicPageModel<T> GetTopPlayedMusicsAllTime<T>(int page, int entitesPerPage, string userId)
        {
            var musics = this.context.Musics.Where(m => m.UserId == userId)
                .OrderByDescending(m => m.PlayedMusics.Count())
                .To<T>();
            return new MusicPageModel<T>()
            {
                Musics = musics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetMusicsPagesCount(musics.Count(), entitesPerPage),
            };
        }

        private int GetMusicsPagesCount(int entityCount, int entitesPerPage)
          => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
