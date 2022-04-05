using MusicHub.Common;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Tests.TestData;
using MusicHub.Web.ViewModels.ReportMusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Services.Mapping;
using MusicHub.Common.Extensions;
using static MusicHub.Common.GlobalConstants;
using FluentAssertions;

namespace MusicHub.Services.Data.Tests
{
    public class ReportMusicServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateReportMusic_WithValidData_ShouldWorkCorrect()
        {
            var reportMusicService = await this.CreateService(new List<ReportMusic>());
            var model = ReportMusicTestsData.CreateModel;
            var userId = ReportMusicTestsData.UserId;

            var reportMusicId = await reportMusicService.Create(model, userId);

            Assert.True(this.context.ReportMusics.Any(m => m.MusicId == model.MusicId
                && m.UserId == userId
                && m.Description == model.Description
                && m.Seen == false
                && m.Type == model.Type
                && m.Id == reportMusicId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetReportsByMusicId_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var reportMusicService = await this.CreateService(reportedMusics);
            var entitesPerPage = PaginationData.ReportsPerPage;
            var musicId = reportedMusics[0].MusicId;

            var actual = reportMusicService.All<ReportViewModel>(musicId, page, entitesPerPage);

            var reportMusics = this.context.ReportMusics
                             .Where(rm => rm.MusicId == musicId)
                             .OrderBy(rm => rm.Seen)
                             .To<ReportViewModel>();
            var expected = new ReportPageModel<ReportViewModel>()
            {
                ReportMusics = reportMusics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(reportMusics.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task Delete_WithValidData()
        {
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var reportMusicService = await this.CreateService(reportedMusics);
            var reportMusicId = reportedMusics[0].Id;

            await reportMusicService.Delete(reportMusicId);

            Assert.False(this.context.ReportMusics.Any(rm => rm.Id == reportMusicId));
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldReturnCurrectData()
        {
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var reportMusicService = await this.CreateService(reportedMusics);
            var reportMusicId = reportedMusics[0].Id;

            var expected = reportedMusics.Where(rm => rm.Id == reportMusicId)
                .FirstOrDefault()
                .To<ReportViewModel>();
            var actual = reportMusicService.GetById<ReportViewModel>(reportMusicId);
            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterReportsByMusicIdWithTypeUserIdAndUsername_WithValidData_ShouldeReturnData(int page)
        {
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var filter = ReportMusicTestsData.FilterModelReportWithTypeUserIdAndUsername;
            var reportMusicService = await this.CreateService(reportedMusics);
            var entitesPerPage = PaginationData.ReportsPerPage;
            var musicId = reportedMusics[0].MusicId;

            var actual = reportMusicService.Filter<ReportViewModel>(musicId, filter, page, entitesPerPage);

            this.CheckFilter(actual, page, entitesPerPage, filter, musicId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterReportsByMusicIdWithTypeUserId_WithValidData_ShouldeReturnData(int page)
        {
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var filter = ReportMusicTestsData.FilterModelReportWithTypeUserId;
            var reportMusicService = await this.CreateService(reportedMusics);
            var entitesPerPage = PaginationData.ReportsPerPage;
            var musicId = reportedMusics[0].MusicId;

            var actual = reportMusicService.Filter<ReportViewModel>(musicId, filter, page, entitesPerPage);

            this.CheckFilter(actual, page, entitesPerPage, filter, musicId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterReportsByMusicIdWithType_WithValidData_ShouldeReturnData(int page)
        {
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var filter = ReportMusicTestsData.FilterModelReportWithType;
            var reportMusicService = await this.CreateService(reportedMusics);
            var entitesPerPage = PaginationData.ReportsPerPage;
            var musicId = reportedMusics[0].MusicId;

            var actual = reportMusicService.Filter<ReportViewModel>(musicId, filter, page, entitesPerPage);

            this.CheckFilter(actual, page, entitesPerPage, filter, musicId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterReportsByMusicIdWithTypeAndUsername_WithValidData_ShouldeReturnData(int page)
        {
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var filter = ReportMusicTestsData.FilterModelReportWithTypeAndUsername;
            var reportMusicService = await this.CreateService(reportedMusics);
            var entitesPerPage = PaginationData.ReportsPerPage;
            var musicId = reportedMusics[0].MusicId;

            var actual = reportMusicService.Filter<ReportViewModel>(musicId, filter, page, entitesPerPage);

            this.CheckFilter(actual, page, entitesPerPage, filter, musicId);
        }

        private void CheckFilter(ReportPageModel<ReportViewModel> actual, int page, int entitesPerPage, ReportFilter filter, string musicId)
        {
            IQueryable<ReportMusic> reportMusics = this.context.ReportMusics
                  .Where(rm => rm.MusicId == musicId);
            reportMusics = this.Filtering(filter, reportMusics);
            reportMusics = this.Order(filter.OrderMethod, reportMusics);
            var expected = new ReportPageModel<ReportViewModel>()
            {
                ReportMusics = reportMusics
                    .To<ReportViewModel>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(reportMusics.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

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
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<ReportMusicService> CreateService(List<ReportMusic> reportMusics)
        {
            var users = ReportMusicTestsData.Users;
            var categories = ReportMusicTestsData.Categories;
            var musics = ReportMusicTestsData.Musics;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Musics.AddRangeAsync(musics);
            await this.context.ReportMusics.AddRangeAsync(reportMusics);
            await this.context.SaveChangesAsync();
            var service = new ReportMusicService(this.context, new PaginationService());
            return service;
        }
    }
}
