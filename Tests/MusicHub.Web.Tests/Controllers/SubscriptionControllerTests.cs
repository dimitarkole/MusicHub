using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.SubscriptionModels;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class SubscriptionControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllSubscriptions_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var subscriptions = SubscriptionTestsData.Subscriptions;
            var subscriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var users = SubscriptionTestsData.Users;
            var userId = SubscriptionTestsData.UserId;
            MyController<SubscriptionController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<SubscriptionPageModel<SubscriptionViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.SubscriptionsPerPage;
                        var service = new SubscriptionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<SubscriptionViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterSubscriptions_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var subscriptions = SubscriptionTestsData.Subscriptions;
            var subscriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var users = SubscriptionTestsData.Users;
            var userId = SubscriptionTestsData.UserId;
            var filter = SubscriptionTestsData.FilterModel;
            MyController<SubscriptionController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<SubscriptionPageModel<SubscriptionViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.SubscriptionsPerPage;
                        var service = new SubscriptionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<SubscriptionViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterSubscriptions_WithAllTypes_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var subscriptions = SubscriptionTestsData.Subscriptions;
            var subscriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var users = SubscriptionTestsData.Users;
            var userId = SubscriptionTestsData.UserId;
            var filter = SubscriptionTestsData.FilterModelWithAllTypes;
            MyController<SubscriptionController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<SubscriptionPageModel<SubscriptionViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.SubscriptionsPerPage;
                        var service = new SubscriptionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<SubscriptionViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterSubscriptions_WithoutType_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var subscriptions = SubscriptionTestsData.Subscriptions;
            var subscriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var users = SubscriptionTestsData.Users;
            var userId = SubscriptionTestsData.UserId;
            var filter = SubscriptionTestsData.FilterModelWithoutType;
            MyController<SubscriptionController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<SubscriptionPageModel<SubscriptionViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.SubscriptionsPerPage;
                        var service = new SubscriptionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<SubscriptionViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreateSubscription_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = SubscriptionTestsData.Users;
            var userId = SubscriptionTestsData.UserId;
            var model = SubscriptionTestsData.CreateModel;
            MyController<SubscriptionController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .StatusCode(result => result
                    .WithStatusCode(StatusCodes.Status201Created)
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            Assert.True(From.Services<ApplicationDbContext>().Subscriptions
                                .Any(m => m.Id == property.GetValue(obj).ToString()));
                        }
                    }));
        }

        [Fact]
        public void CreateSubscription_WithEmptyModel_ShouldReturnBadRequest()
        {
            var users = SubscriptionTestsData.Users;
            var userId = SubscriptionTestsData.UserId;
            var model = SubscriptionTestsData.CreateEmptyModel;
            MyController<SubscriptionController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.Name))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Name field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().Subscriptions
                .Any(m => m.Name == model.Name));
        }

        [Fact]
        public void DeletetMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var subscriptions = SubscriptionTestsData.Subscriptions;
            var subscriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var users = SubscriptionTestsData.Users;
            var userId = SubscriptionTestsData.UserId;
            var deleteSubscriptionId = subscriptions[0].Id;
            MyController<SubscriptionController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteSubscriptionId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Subscriptions
                .Any(m => m.Id == deleteSubscriptionId));
        }

        [Fact]
        public void EditSubscription_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var subscriptions = SubscriptionTestsData.Subscriptions;
            var subscriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var users = SubscriptionTestsData.Users;
            var userId = SubscriptionTestsData.UserId;
            var editSubscriptionId = subscriptions[0].Id;
            var model = SubscriptionTestsData.UpdateModel;
            MyController<SubscriptionController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editSubscriptionId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Subscriptions
                .Any(m => m.Id == editSubscriptionId
                    && m.ModifiedOn != null));
        }

        [Fact]
        public void EditSubscription_WithEmptyModel_ShouldReturnBadRequest()
        {
            var subscriptions = SubscriptionTestsData.Subscriptions;
            var subscriptionClaims = SubscriptionTestsData.SubscriptionsClaims();
            var users = SubscriptionTestsData.Users;
            var userId = SubscriptionTestsData.UserId;
            var editSubscriptionId = subscriptions[0].Id;
            var model = SubscriptionTestsData.UpdateModelEmptyModel;
            MyController<SubscriptionController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editSubscriptionId, model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.Name))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Name field is required")).Any());
                    }
                }));

            Assert.True(From.Services<ApplicationDbContext>().Subscriptions
                .Any(m => m.Id == editSubscriptionId
                    && m.ModifiedOn != null));
        }
    }
}
