namespace MusicHub.Services.Data.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Analysis.Interfaces;
    using MusicHub.Web.ViewModels.ChartModels;
    using VisioForge.Shared.MediaFoundation.OPM;
    using static MusicHub.Common.AnalysisConstants;
    public class CategoryAnalysisService : ICategoryAnalysisService
    {
        private readonly ApplicationDbContext context;

        public CategoryAnalysisService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public PieChartViewModel GetCategoriesByPlayedMusicsCountAllTime(ChartFilterData filter)
        {
            return new PieChartViewModel()
            {
                Series = this.context.PlayedMusics
                    .GroupBy(g => g.Music.Category.Name)
                    .Select(r => new PieChartSeriesViewModel()
                    {
                        Name = r.Key,
                        Value = r.Count(),
                    })
                    .OrderByDescending(d => d.Name)
                    .ToList(),
                Name = CategoryAnalysis.GetCategoriesByPlayedMusicsCountAllTime,
            };
        }

        public PieChartViewModel GetCategoriesWithMusicsAllTime()
            => new PieChartViewModel()
            {
                Name = CategoryAnalysis.GetCategoriesWithMusicsAllTime,
                Series = this.context.Categories
                            .OrderBy(c => c.Name)
                            .Select(c => new PieChartSeriesViewModel()
                            {
                                Name = c.Name,
                                Value = c.Musics.Count(),
                            })
                            .ToList(),
            };

        public PieChartViewModel GetCategoriesWithMusicsCount(ChartFilterData filter)
            => new PieChartViewModel()
                {
                    Name = CategoryAnalysis.GetCategoriesWithMusicsCount,
                    Series = this.context.Categories
                        .OrderBy(c => c.Name)
                        .Select(c => new PieChartSeriesViewModel()
                        {
                            Name = c.Name,
                            Value = c.Musics
                                .Where(m => (filter.CreatedOnFrom != filter.CreatedOnTo
                                                && m.CreatedOn > filter.CreatedOnFrom
                                                && m.CreatedOn < filter.CreatedOnTo)
                                                || (filter.CreatedOnFrom == filter.CreatedOnTo))
                                .Count(),
                        })
                        .ToList(),
                };

        public PieChartViewModel GetCreatedCategoriesCountsByMonths(ChartFilterData filter)
           => new PieChartViewModel()
           {
               Name = CategoryAnalysis.GetCreatedCategoriesCountsByMonths,
               Series = this.context.Categories
                        .OrderBy(c => c.Name)
                        .Where(c => (filter.CreatedOnFrom != filter.CreatedOnTo
                                                && c.CreatedOn > filter.CreatedOnFrom
                                                && c.CreatedOn < filter.CreatedOnTo)
                                                || (filter.CreatedOnFrom == filter.CreatedOnTo))
                        .GroupBy(c => c.CreatedOn.Month )
                        .Select(c => new PieChartSeriesViewModel()
                        {
                            Name = c.Key + "",
                            Value = c.Count(),
                        })
                        .ToList(),
           };

        public LineChartViewModel GetTopCategoriesByPlayedMusicsCountForMonths(ChartFilterData filter)
        {
            var today = DateTime.UtcNow;
            var treeMonthsBefore = today.AddMonths(-filter.CountOfMonthsBefore);

            var categories = this.context.Categories
                .Include(c => c.Musics)
                .OrderBy(c => c.Name);
            var chartData = new List<LineChartDataViewModel>();
            foreach (var category in categories)
            {
                var series = this.context.PlayedMusics
                        .Where(pm => pm.Music.CategoryId == category.Id
                                 && pm.CreatedOn > treeMonthsBefore
                                 && pm.CreatedOn < today)
                        .GroupBy(g => g.CreatedOn.Month)
                        .Select(r => new LineChartSeriesViewModel()
                        {
                            Name = r.Key +"",
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

        public LineChartViewModel GetTopCategoriesByUploadMusicsCountForMonths(ChartFilterData filter)
        {
            var today = DateTime.UtcNow;
            var treeMonthsBefore = today.AddMonths(-filter.CountOfMonthsBefore);

            var categories = this.context.Categories
                .Include(c => c.Musics)
                .OrderBy(c => c.Name);
            var chartData = new List<LineChartDataViewModel>();
            foreach (var category in categories)
            {
                var data = new LineChartDataViewModel()
                {
                    Name = category.Name,
                    Series = category.Musics
                        .Where(m => m.CreatedOn > treeMonthsBefore
                            && m.CreatedOn < today)
                        .GroupBy(g => g.CreatedOn.ToString("MMM"))
                        .Select(r => new LineChartSeriesViewModel()
                        {
                            Name = r.Key,
                            Value = r.Count(),
                        })
                        .ToList(),
                };

                chartData.Add(data);
            }

            return new LineChartViewModel()
            {
                Data = chartData
                    .OrderByDescending(d => d.Series.Sum(s => s.Value))
                    .Take(filter.CountOfSeries)
                    .ToList(),
                Name = CategoryAnalysis.GetTopCategoriesByUploadMusicsCountForMonths,
                XAxisLabel = CategoryAnalysis.MonthsXLabel,
                YAxisLabel = CategoryAnalysis.MusicYLabel,
            };
        }
    }
}
