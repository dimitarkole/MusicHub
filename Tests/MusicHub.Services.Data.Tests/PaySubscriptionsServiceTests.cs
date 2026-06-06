using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
using MusicHub.Web.ViewModels.PaySubscriptionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Services.Mapping;
using MusicHub.Common.Extensions;
using MusicHub.Common;
using FluentAssertions;

namespace MusicHub.Services.Data.Tests
{
    public class PaySubscriptionsServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreatePaySubscription_WithValidData_ShouldWorkCorrect()
        {
            var service = await this.CreateService(new List<PaySubscription>());
            var model = PaySubscriptionTestsData.CreateModel;
            var userId = PaySubscriptionTestsData.UserId;
            await service.Create(model, userId);
            Assert.True(this.context.PaySubscriptions.Any(ps => ps.ValidTo == model.ValidTo
                && ps.UserId == userId
                && ps.ValidFrom == model.ValidFrom
                && ps.SubscriptionId == model.SubscriptionId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetPaySubscription_WithValidData_ShouldeReturnPublicPlaylist(int page)
        {
            var service = await this.CreateService(PaySubscriptionTestsData.PaySubscriptions());
            var entitesPerPage = PaginationData.PaySubscriptionsPerPage;
            var actual = service.All<PaySubscriptionViewModel>(page, entitesPerPage);
            IQueryable<PaySubscription> paySubscriptions = this.context.PaySubscriptions;
            var paySubscriptionsT = paySubscriptions.To<PaySubscriptionViewModel>();
            var expected = new PaySubscriptionPageModel<PaySubscriptionViewModel>()
            {
                PaySubscriptions = paySubscriptionsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(paySubscriptionsT.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetOwnPaySubscription_WithValidData_ShouldeReturnPublicPlaylist(int page)
        {
            var service = await this.CreateService(PaySubscriptionTestsData.PaySubscriptions());
            var entitesPerPage = PaginationData.PaySubscriptionsPerPage;
            var userId = PaySubscriptionTestsData.UserId;
            var actual = service.All<PaySubscriptionViewModel>(page, entitesPerPage, userId);
            IQueryable<PaySubscription> paySubscriptions = this.context.PaySubscriptions;
            if (!string.IsNullOrEmpty(userId))
            {
                paySubscriptions = paySubscriptions.Where(s => s.UserId == userId);
            }

            var paySubscriptionsT = paySubscriptions.To<PaySubscriptionViewModel>();
            var expected = new PaySubscriptionPageModel<PaySubscriptionViewModel>()
            {
                PaySubscriptions = paySubscriptionsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(paySubscriptionsT.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterPaySubscription_WithValidData_ShouldeReturnPublicPlaylist(int page)
        {
            var service = await this.CreateService(PaySubscriptionTestsData.PaySubscriptions());
            var entitesPerPage = PaginationData.PaySubscriptionsPerPage;
            var filterModel = PaySubscriptionTestsData.FilterModel;
            var actual = service.Filter<PaySubscriptionViewModel>(page, entitesPerPage, filterModel);
            this.CheckFilterData(actual, page, entitesPerPage, filterModel);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterOwnPaySubscription_WithValidData_ShouldeReturnPublicPlaylist(int page)
        {
            var service = await this.CreateService(PaySubscriptionTestsData.PaySubscriptions());
            var entitesPerPage = PaginationData.PaySubscriptionsPerPage;
            var filterModel = PaySubscriptionTestsData.FilterModel;
            var userId = PaySubscriptionTestsData.UserId;
            var actual = service.Filter<PaySubscriptionViewModel>(page, entitesPerPage, filterModel, userId);
            this.CheckFilterData(actual, page, entitesPerPage, filterModel, userId);
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var paySubscription = PaySubscriptionTestsData.PaySubscriptions();
            var service = await this.CreateService(paySubscription);
            var paySubscriptionId = paySubscription[0].Id;
            var result = service.GetById<PaySubscriptionViewModel>(paySubscriptionId);
            Assert.True(this.context.PaySubscriptions.Any(ps => ps.Id == paySubscriptionId
                  && ps.ValidFrom == result.ValidFrom
                  && ps.ValidTo == result.ValidTo
                  && ps.SubscriptionId == result.SubscriptionId));
        }

        [Fact]
        public async Task IsPaySubscriptionOwned_WithValidData_ShouldeReturnTrue()
        {
            var paySubscription = PaySubscriptionTestsData.PaySubscriptions();
            var service = await this.CreateService(paySubscription);
            var paySubscriptionId = paySubscription[0].Id;
            var userId = paySubscription[0].UserId;
            Assert.True(service.IsOwned(paySubscriptionId, userId));
        }

        [Fact]
        public async Task IsPaySubscriptionOwned_WithValidData_ShouldeReturnFalse()
        {
            var paySubscription = PaySubscriptionTestsData.PaySubscriptions();
            var service = await this.CreateService(paySubscription);
            var paySubscriptionId = paySubscription[0].Id;
            var ownerUserId = paySubscription[0].UserId;
            var userId = paySubscription.Where(ps => ps.UserId != ownerUserId)
                .FirstOrDefault()
                .UserId;
            Assert.False(service.IsOwned(paySubscriptionId, userId));
        }

        [Fact]
        public async Task UpdateMusic_WithValidData_ShouldWorkCorrect()
        {
            var paySubscription = PaySubscriptionTestsData.PaySubscriptions();
            var service = await this.CreateService(paySubscription);
            var paySubscriptionId = paySubscription[0].Id;
            var model = PaySubscriptionTestsData.UpdateModel;
            await service.Update(paySubscriptionId, model);
            Assert.True(this.context.PaySubscriptions.Any(ps => ps.Id == paySubscriptionId
                && ps.SubscriptionId == model.SubscriptionId
                && ps.ValidFrom == model.ValidFrom
                && ps.ValidTo == model.ValidTo));
        }

        private void CheckFilterData(PaySubscriptionPageModel<PaySubscriptionViewModel> actual, int page,int entitesPerPage, PaySubscriptionFilter filter, string userId = null)
        {
            IQueryable<PaySubscription> paySubscriptions = this.context.PaySubscriptions;
            if (!string.IsNullOrEmpty(userId))
            {
                paySubscriptions = paySubscriptions.Where(s => s.UserId == userId);
            }

            paySubscriptions = this.Filtering(filter, paySubscriptions);
            paySubscriptions = this.Ordering(filter.OrderMethod, paySubscriptions);
            var paySubscriptionsT = paySubscriptions.To<PaySubscriptionViewModel>();
            var expected = new PaySubscriptionPageModel<PaySubscriptionViewModel>()
            {
                PaySubscriptions = paySubscriptionsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(paySubscriptionsT.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        private IQueryable<PaySubscription> Filtering(PaySubscriptionFilter filter, IQueryable<PaySubscription> paySubscriptions)
        {
            if (filter.ValidFrom != null)
            {
                paySubscriptions = paySubscriptions.Where(ps => ps.ValidFrom >= filter.ValidFrom);
            }

            if (filter.ValidFrom != null)
            {
                paySubscriptions = paySubscriptions.Where(ps => ps.ValidTo <= filter.ValidTo);
            }

            return paySubscriptions;
        }

        private IQueryable<PaySubscription> Ordering(OrderMethod orderMethod, IQueryable<PaySubscription> paySubscriptions)
        {
            if (orderMethod == OrderMethod.DateDesc)
            {
                return paySubscriptions.OrderByDescending(ps => ps.ValidFrom);
            }

            return paySubscriptions.OrderBy(ps => ps.ValidFrom);
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<PaySubscriptionService> CreateService(List<PaySubscription> paySubscriptions)
        {
            await this.context.Users.AddRangeAsync(PaySubscriptionTestsData.Users);
            await this.context.Subscriptions.AddRangeAsync(PaySubscriptionTestsData.Subscriptions);
            await this.context.PaySubscriptions.AddRangeAsync(paySubscriptions);
            await this.context.SaveChangesAsync();
            var service = new PaySubscriptionService(this.context, new PaginationService());
            return service;
        }
    }
}
