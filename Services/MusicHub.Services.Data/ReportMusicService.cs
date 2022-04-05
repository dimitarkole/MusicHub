namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.ReportMusicModels;

    public class ReportMusicService : IReportMusicService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public ReportMusicService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public ReportPageModel<T> All<T>(string musicId, int page, int entitesPerPage)
        {
            var reportMusics = this.context.ReportMusics
                    .Where(rm => rm.MusicId == musicId)
                    .OrderBy(rm => rm.Seen)
                    .To<T>();
            return new ReportPageModel<T>()
            {
                ReportMusics = reportMusics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(reportMusics.Count(), entitesPerPage),
            };
        }

        public async Task<string> Create(ReportMusicCreateInputModel model, string userId)
        {
            var reportMusic = model.To<ReportMusic>();
            reportMusic.UserId = userId;
            reportMusic.Seen = false;
            await this.context.ReportMusics.AddAsync(reportMusic);
            await this.context.SaveChangesAsync();
            return reportMusic.Id;
        }

        public async Task Delete(string id)
        {
            var reportMusic = this.context.ReportMusics.Find(id);
            this.context.ReportMusics.Remove(reportMusic);
            await this.context.SaveChangesAsync();
        }

        public ReportPageModel<T> Filter<T>(string musicId, ReportFilter filter, int page, int entitesPerPage)
        {
            IQueryable<ReportMusic> reportMusics = this.context.ReportMusics
                    .Where(rm => rm.MusicId == musicId);
            reportMusics = this.Filtering(filter, reportMusics);
            reportMusics = this.Order(filter.OrderMethod, reportMusics);
            return new ReportPageModel<T>()
            {
                ReportMusics = reportMusics
                    .To<T>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(reportMusics.Count(), entitesPerPage),
            };
        }

        public T GetById<T>(string id)
            => this.context.ReportMusics
                .Where(rm => rm.Id == id)
                .To<T>()
                .FirstOrDefault();

        private IQueryable<ReportMusic> Order(OrderMethod orderMethod, IQueryable<ReportMusic> reportMusics)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                reportMusics = reportMusics.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                reportMusics = reportMusics.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.UserNameAsc)
            {
                reportMusics = reportMusics.OrderBy(s => s.User.UserName);
            }
            else if (orderMethod == OrderMethod.UserNameDesc)
            {
                reportMusics = reportMusics.OrderByDescending(s => s.User.UserName);
            }
            else if (orderMethod == OrderMethod.SeenFirst)
            {
                reportMusics = reportMusics.OrderBy(s => !s.Seen);
            }
            else
            {
                reportMusics = reportMusics.OrderBy(s => s.Seen);
            }

            return reportMusics;
        }

        private IQueryable<ReportMusic> Filtering(ReportFilter filter, IQueryable<ReportMusic> reportMusics)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                reportMusics = reportMusics.Where(rm => rm.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.UserName))
            {
                reportMusics = reportMusics.Where(rm => rm.User.UserName.ToLower().Contains(filter.UserName.ToLower()));
            }

            if (filter.Type != ReportMusicType.NoType)
            {
                reportMusics = reportMusics.Where(rp => rp.Type == filter.Type);
            }

            return reportMusics;
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
