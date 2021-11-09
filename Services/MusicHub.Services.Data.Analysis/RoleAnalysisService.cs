namespace MusicHub.Services.Data.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.SqlServer;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MusicHub.Data;
    using MusicHub.Services.Data.Analysis.Interfaces;
    using MusicHub.Web.ViewModels.ChartModels;
    using static MusicHub.Common.AnalysisConstants;

    public class RoleAnalysisService : IRoleAnalysisService
    {
        private readonly ApplicationDbContext context;

        public RoleAnalysisService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public PieChartViewModel GetRolesByCountOfPermissions()
               => new PieChartViewModel()
               {
                   Series = this.context.RolePermissions
                                    .GroupBy(rp => rp.Role.Name)
                                    .Select(r => new PieChartSeriesViewModel()
                                    {
                                        Name = r.Key,
                                        Value = r.Count(),
                                    })
                                    .OrderByDescending(d => d.Name)
                                    .ToList(),
                   Name = RoleAnalysis.GetRolesByCountOfPermissions,
               };

        public PieChartViewModel GetRolesByCountOfUsers()
             => new PieChartViewModel()
             {
                 Series = this.context.UserRoles
                            .GroupBy(ur => ur.Role.Name)
                            .Select(r => new PieChartSeriesViewModel()
                            {
                                Name = r.Key,
                                Value = r.Count(),
                            })
                            .OrderByDescending(d => d.Name)
                            .ToList(),
                 Name = RoleAnalysis.GetRolesByCountOfUsers,
             };

        public LineChartViewModel GetRolesByUsersForMonths(ChartFilterData filter)
        {
            var today = DateTime.UtcNow;
            var beforeDate = today.AddMonths(-filter.CountOfMonthsBefore);

            var roles = this.context.Roles;
            var chartData = new List<LineChartDataViewModel>();
            foreach (var role in roles)
            {
                var series = this.context.UserRoles
                    .Include(ur => ur.User)
                    .Include(ur => ur.Role)
                    .Where(ur => ur.RoleId == role.Id
                            && ((beforeDate != today && ur.User.CreatedOn > beforeDate
                            && ur.User.CreatedOn < today) || beforeDate == today))
                    .GroupBy(g => new { Month = g.User.CreatedOn.Month , Year = g.User.CreatedOn.Year})
                    .Select(r => new LineChartSeriesViewModel()
                    {
                        Name = r.Key.Month + "/" + r.Key.Year,
                        Value = r.Count(),
                    })
                    .ToList();

                var data = new LineChartDataViewModel()
                {
                    Name = role.Name,
                    Series = series,
                };

                chartData.Add(data);
            }

            return new LineChartViewModel()
            {
                Data = chartData
                    .ToList(),
                Name = RoleAnalysis.GetRolesByUsersForMonths,
                XAxisLabel = CategoryAnalysis.MonthsXLabel,
                YAxisLabel = CategoryAnalysis.MusicYLabel,
            };
        }

        public LineChartViewModel GetRolesByUsersLastWeek()
        {
            var today = DateTime.UtcNow;
            var beforeDate = today.AddDays(-TimePeriods.Week);

            var roles = this.context.Roles;
            var chartData = new List<LineChartDataViewModel>();
            foreach (var role in roles)
            {
                var series = this.context.UserRoles
                    .Include(ur => ur.User)
                    .Include(ur => ur.Role)
                    .Where(ur => ur.RoleId == role.Id
                            && ((beforeDate != today && ur.User.CreatedOn > beforeDate
                            && ur.User.CreatedOn < today) || beforeDate == today))
                    .GroupBy(g => g.User.CreatedOn.Day)
                    .Select(r => new LineChartSeriesViewModel()
                    {
                        Name = r.Key + "",
                        Value = r.Count(),
                    })
                    .ToList();

                var data = new LineChartDataViewModel()
                {
                    Name = role.Name,
                    Series = series,
                };

                chartData.Add(data);
            }

            return new LineChartViewModel()
            {
                Data = chartData
                    .ToList(),
                Name = RoleAnalysis.GetRolesByUsersLastWeek,
                XAxisLabel = CategoryAnalysis.MonthsXLabel,
                YAxisLabel = CategoryAnalysis.MusicYLabel,
            };
        }

        public LineChartViewModel GetRolesByAverageCountOfUsersForWeek(ChartFilterData filter)
        {
            var today = filter.CreatedOnTo;
            var beforeDate = filter.CreatedOnFrom;
            var countOfWeeks = today != beforeDate ?
                (today - beforeDate).TotalDays / 7
                : (today - this.context.Users.OrderBy(u => u.CreatedOn).FirstOrDefault().CreatedOn).TotalDays / 7;
            var roles = this.context.Roles;
            var chartData = new List<LineChartDataViewModel>();
            foreach (var role in roles)
            {
                var series = this.context.UserRoles
                    .Include(ur => ur.User)
                    .Include(ur => ur.Role)
                    .Where(ur => ur.RoleId == role.Id
                            && ((beforeDate != today && ur.User.CreatedOn > beforeDate
                            && ur.User.CreatedOn < today) || beforeDate == today))
                    .GroupBy(g => g.User.CreatedOn.Day)
                    .Select(r => new LineChartSeriesViewModel()
                    {
                        Name = r.Key + "",
                        Value = r.Count() / countOfWeeks,
                    })
                    .ToList();

                var data = new LineChartDataViewModel()
                {
                    Name = role.Name,
                    Series = series,
                };

                chartData.Add(data);
            }

            return new LineChartViewModel()
            {
                Data = chartData
                    .ToList(),
                Name = RoleAnalysis.GetRolesByAverageCountOfUsersForWeek,
                XAxisLabel = CategoryAnalysis.MonthsXLabel,
                YAxisLabel = CategoryAnalysis.MusicYLabel,
            };
        }

    }
}
