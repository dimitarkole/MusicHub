namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;
    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.BellModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

    public class BellServiceTests : BaseTestClass
    {
        // bellings - users who I belling
        // bellers - user who belled me
        public int EntitesPerPage = PaginationData.BellsPerPage;

        [Fact]
        public async Task Create_WithValidData_ShouldWorkCorrect()
        {
            var bellService = await this.CreateService(new List<Bell>());
            var model = BellTestsData.CreateModel;
            var userId = BellTestsData.CreateUserId;
            await bellService.Create(model, userId);
            Assert.True(this.context.Bells.Any(b => b.BellerId == userId
                && b.BellingId == model.BellingId));
        }

        [Fact]
        public async Task IsOwnedTrue_WithValidData_ShouldWorkCorrect()
        {
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var bell = bells[0];
            Assert.True(bellService.IsOwned(bell.Id, bell.BellerId));
        }

        [Fact]
        public async Task IsOwnedFalse_WithValidData_ShouldWorkCorrect()
        {
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var bellingId = bells[0].BellingId;
            var bellId = bells
                .Where(f => f.BellingId != bellingId)
                .FirstOrDefault()
                .Id;

            Assert.False(bellService.IsOwned(bellId, bellingId));
        }

        [Fact]
        public async Task IsBellingTrue_WithValidData_ShouldWorkCorrect()
        {
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var bell = bells[0];
            Assert.True(bellService.IsBelling(bell.BellerId, bell.BellingId));
        }

        [Fact]
        public async Task IsBellingFalse_WithValidData_ShouldWorkCorrect()
        {
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var userId = bells[0].BellingId;
            var bellId = bells
                .Where(b => !b.Beller.Bellings.Any(b2 => b2.BellerId != userId))
                .FirstOrDefault()
                .BellerId;

            Assert.False(bellService.IsBelling(bellId, userId));
        }

        [Fact]
        public async Task Delete_WithValidData_ShouldWorkCorrect()
        {
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var bellId = bells[0].Id;

            await bellService.Delete(bellId);

            Assert.False(this.context.Bells.Any(f => f.Id == bellId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllBelling_WithValidData_ShouldeReturn(int page)
        {
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var users = UserTestsData.Users;
            var userId = users[0].Id;
            var actual = bellService.AllBellings<BellingViewModel>(page, this.EntitesPerPage, userId);

            this.CheckFilterBellings(page, new BellFilterInputModel(), userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllBellers_WithValidData_ShouldeReturn(int page)
        {
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = bellService.AllBellers<BellerViewModel>(page, this.EntitesPerPage, userId);

            this.CheckFilterBellers(page, new BellFilterInputModel(), userId, actual);
        }

        [Fact]
        public async Task GetbellingInfo_WithValidData_ShouldWorkCorrect()
        {
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var bell = bells[0];

            var result = bellService.GetBell<BellerViewModel>(bell.BellerId, bell.BellingId);

            Assert.Equal(bell.Id, result.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterBelling_WithValidData_ShouldeReturn(int page)
        {
            var filter = BellTestsData.FilterModelUserName;
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = bellService.FilterBellings<BellingViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterBellings(page, filter, userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterBellers_WithValidData_ShouldeReturn(int page)
        {
            var filter = BellTestsData.FilterModelUserName;
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = bellService.FilterBellers<BellerViewModel>(page, this.EntitesPerPage, filter, userId);
            this.CheckFilterBellers(page, filter, userId, actual);
        }

        private void CheckFilterBellers(int page, BellFilterInputModel filter, string userId, BellerPageModel<BellerViewModel> actual)
        {
            var bellers = this.context.Bells.Where(b => b.BellingId == userId);
            bellers = this.FilteringBellers(filter, bellers);
            bellers = this.OrederBellers(filter.OrderMethod, bellers);
            var bellersT = bellers.To<BellerViewModel>();
            var expected = new BellerPageModel<BellerViewModel>()
            {
                Bellers = bellersT
                    .GetPage(page, this.EntitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetbellPagesCount(bellersT.Count(), this.EntitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        private void CheckFilterBellings(int page, BellFilterInputModel filter, string userId, BellingPageModel<BellingViewModel> actual)
        {
            var bellings = this.context.Bells
                   .Where(f => f.BellerId == userId);

            bellings = this.FilteringBellings(filter, bellings);
            bellings = this.OrederBellings(filter.OrderMethod, bellings);
            var bellersT = bellings.To<BellingViewModel>();

            var expected = new BellingPageModel<BellingViewModel>()
            {
                Bellings = bellersT
                    .GetPage(page, this.EntitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetbellPagesCount(bellings.Count(), this.EntitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterBellingLastName_WithValidData_ShouldeReturn(int page)
        {
            var filter = BellTestsData.FilterModelLastName;
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = bellService.FilterBellings<BellingViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterBellings(page, filter, userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterBellersLastName_WithValidData_ShouldeReturn(int page)
        {
            var filter = BellTestsData.FilterModelLastName;
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = bellService.FilterBellers<BellerViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterBellers(page, filter, userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterbellingFirstName_WithValidData_ShouldeReturn(int page)
        {
            var filter = BellTestsData.FilterModelFirstName;
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = bellService.FilterBellings<BellingViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterBellings(page, filter, userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterBellersFirstName_WithValidData_ShouldeReturn(int page)
        {
            var filter = BellTestsData.FilterModelFirstName;
            var bells = BellTestsData.Bells();
            var bellService = await this.CreateService(bells);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = bellService.FilterBellers<BellerViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterBellers(page, filter, userId, actual);
        }

        private int GetbellsCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private int GetbellPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private IQueryable<Bell> FilteringBellings(BellFilterInputModel filter, IQueryable<Bell> bells)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                bells = bells.Where(b => b.Belling.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                bells = bells.Where(b => b.Belling.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                bells = bells.Where(b => b.Belling.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return bells;
        }

        private IQueryable<Bell> FilteringBellers(BellFilterInputModel filter, IQueryable<Bell> bells)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                bells = bells.Where(b => b.Beller.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                bells = bells.Where(b => b.Beller.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                bells = bells.Where(b => b.Beller.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return bells;
        }

        private IQueryable<Bell> OrederBellers(OrderMethod orderMethod, IQueryable<Bell> bells)
        {
            if (orderMethod == OrderMethod.NameAsc)
            {
                bells = bells.OrderBy(b => b.Beller.UserName);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                bells = bells.OrderByDescending(b => b.Beller.UserName);
            }
            else if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                bells = bells.OrderBy(b => b.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                bells = bells.OrderByDescending(b => b.CreatedOn);
            }

            return bells;
        }

        private IQueryable<Bell> OrederBellings(OrderMethod orderMethod, IQueryable<Bell> bells)
        {
            if (orderMethod == OrderMethod.NameDesc)
            {
                bells = bells.OrderByDescending(b => b.Belling.UserName);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                bells = bells.OrderBy(b => b.Belling.UserName);
            }
            else if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                bells = bells.OrderBy(b => b.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                bells = bells.OrderByDescending(b => b.CreatedOn);
            }

            return bells;
        }

        private async Task<BellService> CreateService(List<Bell> bells)
        {
            var users = BellTestsData.Users;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Bells.AddRangeAsync(bells);
            await this.context.SaveChangesAsync();
            var service = new BellService(this.context, new PaginationService());

            return service;
        }
    }
}
