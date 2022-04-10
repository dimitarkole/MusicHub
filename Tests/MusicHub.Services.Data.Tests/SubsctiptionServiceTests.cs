namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.SubscriptionModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

    public class SubsctiptionServiceTests : BaseTestClass
    {
        [Fact]
        public async Task Create_WithValidData_ShouldWorkCorrectly()
        {
            var service = await this.CreateService(new List<Subscription>(), new List<SubscriptionClaim>());
            var model = SubscriptionTestsData.CreateModel;
            await service.Create(model);
            Assert.True(this.context.Subscriptions.Any(s => s.Name == model.Name
                && s.Type == model.Type
                && s.Price == model.Price));
            foreach (var claim in model.SubcriptionClaims)
            {
                Assert.True(this.context.SubscriptionClaims.Any(sc => sc.Description == claim.Description));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllSubscriptions_WithValidData_ShouldeReturnCurrectData(int page)
        {
            var subcriptions = SubscriptionTestsData.Subscriptions;
            var subcriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var service = await this.CreateService(subcriptions, subcriptionClaims);
            var entitesPerPage = PaginationData.SubscriptionsPerPage;
            var actual = service.All<SubscriptionViewModel>(page, entitesPerPage);
            var subscriptions = this.context.Subscriptions
                  .OrderBy(s => s.Name)
                  .To<SubscriptionViewModel>();
            var expected = new SubscriptionPageModel<SubscriptionViewModel>()
            {
                Subscriptions = subscriptions
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(subscriptions.Count(), entitesPerPage),
            };
            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetFilterSubscriptions_WithValidData_ShouldeReturnCurrectData(int page)
        {
            var subcriptions = SubscriptionTestsData.Subscriptions;
            var subcriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var service = await this.CreateService(subcriptions, subcriptionClaims);
            var entitesPerPage = PaginationData.SubscriptionsPerPage;
            var filter = SubscriptionTestsData.FilterModel;
            var actual = service.Filter<SubscriptionViewModel>(page, entitesPerPage, filter);
            this.CheckFilter(actual, page, entitesPerPage, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetFilterSubscriptions_WithoutType_WithValidData_ShouldeReturnCurrectData(int page)
        {
            var subcriptions = SubscriptionTestsData.Subscriptions;
            var subcriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var service = await this.CreateService(subcriptions, subcriptionClaims);
            var entitesPerPage = PaginationData.SubscriptionsPerPage;
            var filter = SubscriptionTestsData.FilterModelWithoutType;
            var actual = service.Filter<SubscriptionViewModel>(page, entitesPerPage, filter);
            this.CheckFilter(actual, page, entitesPerPage, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetFilterSubscriptions_WithAllType_WithValidData_ShouldeReturnCurrectData(int page)
        {
            var subcriptions = SubscriptionTestsData.Subscriptions;
            var subcriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var service = await this.CreateService(subcriptions, subcriptionClaims);
            var entitesPerPage = PaginationData.SubscriptionsPerPage;
            var filter = SubscriptionTestsData.FilterModelWithAllTypes;
            var actual = service.Filter<SubscriptionViewModel>(page, entitesPerPage, filter);
            this.CheckFilter(actual, page, entitesPerPage, filter);
        }

        [Fact]
        public async Task DeleteSubcription_WithValidData_ShouldWorkCorrect()
        {
            var subcriptions = SubscriptionTestsData.Subscriptions;
            var subcriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var service = await this.CreateService(subcriptions, subcriptionClaims);
            var subcriptionId = SubscriptionTestsData.SubscriptionId;
            var deletedSubscriptionClaims = subcriptionClaims.Where(sc => sc.SubcriptionId == subcriptionId);
            await service.Delete(subcriptionId);
            Assert.False(this.context.Subscriptions.Any(c => c.Id == subcriptionId));
            foreach (var claim in deletedSubscriptionClaims)
            {
                Assert.False(this.context.SubscriptionClaims.Any(sc => sc.Description == claim.Description));
            }
        }

        [Fact]
        public async Task Update_WithValidData_ShouldWorkCorrect()
        {
            var subcriptions = SubscriptionTestsData.Subscriptions;
            var subcriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var service = await this.CreateService(subcriptions, subcriptionClaims);
            var subcriptionId = SubscriptionTestsData.SubscriptionId;
            var model = SubscriptionTestsData.UpdateModel;
            var removedSubscriptionClaims = subcriptionClaims.Where(sc => sc.SubcriptionId == subcriptionId);
            var result = service.Update(subcriptionId, model);
            Assert.True(this.context.Subscriptions.Any(s => s.Id == subcriptionId
                && s.Name == model.Name
                && s.Type == model.Type
                && s.Price == model.Price));
            foreach (var claim in model.SubcriptionClaims)
            {
                Assert.True(this.context.SubscriptionClaims.Any(sc => sc.Description == claim.Description));
            }

            foreach (var claim in removedSubscriptionClaims)
            {
                Assert.False(this.context.SubscriptionClaims.Any(sc => sc.Id == claim.Id));
            }
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var subcriptions = SubscriptionTestsData.Subscriptions;
            var subcriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var service = await this.CreateService(subcriptions, subcriptionClaims);
            var subcriptionId = SubscriptionTestsData.SubscriptionId;
            var actual = service.GetById<SubscriptionViewModel>(subcriptionId);
            var expected = this.context.Subscriptions
                .Where(s => s.Id == subcriptionId)
                .To<SubscriptionViewModel>()
                .FirstOrDefault();
            expected.Should().BeEquivalentTo(actual);
        }

        private void CheckFilter(SubscriptionPageModel<SubscriptionViewModel> actual, int page, int entitesPerPage, SubscriptionFilter filter, string userId = null)
        {
            IQueryable<Subscription> subscriptions = this.context.Subscriptions
                       .OrderBy(s => s.Name);
            if (!string.IsNullOrEmpty(filter.Name))
            {
                subscriptions = subscriptions.Where(s => s.Name.Contains(filter.Name));
            }

            if (filter.Type != SubscriptionType.All)
            {
                subscriptions = subscriptions.Where(s => s.Type == filter.Type);
            }

            var expected = new SubscriptionPageModel<SubscriptionViewModel>()
            {
                Subscriptions = subscriptions
                    .To<SubscriptionViewModel>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(subscriptions.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<SubscriptionService> CreateService(List<Subscription> subscriptions, List<SubscriptionClaim> subscriptionClaims)
        {
            await this.context.Subscriptions.AddRangeAsync(subscriptions);
            await this.context.SubscriptionClaims.AddRangeAsync(subscriptionClaims);
            await this.context.SaveChangesAsync();
            return new SubscriptionService(this.context, new PaginationService());
        }
    }
}
