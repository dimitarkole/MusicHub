using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.PaySubscriptionModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class PaySubscriptionsControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllPaySubscriptions_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var userId = PaySubscriptionTestsData.UserId;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithData(users)
                .WithData(paySubscriptions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PaySubscriptionPageModel<PaySubscriptionViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PaySubscriptionsPerPage;
                        var service = new PaySubscriptionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<PaySubscriptionViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllOwnPaySubscriptions_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var userId = PaySubscriptionTestsData.UserId;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithData(users)
                .WithData(paySubscriptions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwn(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PaySubscriptionPageModel<PaySubscriptionViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PaySubscriptionsPerPage;
                        var service = new PaySubscriptionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<PaySubscriptionViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetFilterPaySubscriptions_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var userId = PaySubscriptionTestsData.UserId;
            var filter = PaySubscriptionTestsData.FilterModel;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithData(users)
                .WithData(paySubscriptions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PaySubscriptionPageModel<PaySubscriptionViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PaySubscriptionsPerPage;
                        var service = new PaySubscriptionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<PaySubscriptionViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetFilterOwnPaySubscriptions_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var userId = PaySubscriptionTestsData.UserId;
            var filter = PaySubscriptionTestsData.FilterModel;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithData(users)
                .WithData(paySubscriptions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterOwn(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PaySubscriptionPageModel<PaySubscriptionViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PaySubscriptionsPerPage;
                        var service = new PaySubscriptionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<PaySubscriptionViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreatePaySubscription_WithDataInTheDb_ShouldReturnOkWithPaySubscriptionId()
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var userId = PaySubscriptionTestsData.UserId;
            // var paySubscriptionsId = paySubscriptions[0];
            var model = PaySubscriptionTestsData.CreateModel;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithData(users)
                .WithData(paySubscriptions)
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
                            if (property.Name == "paySubscriptionId")
                            {
                                Assert.True(From.Services<ApplicationDbContext>().PaySubscriptions
                                    .Any(ps => ps.Id == property.GetValue(obj).ToString()
                                        && ps.ValidFrom == model.ValidFrom
                                        && ps.UserId == userId
                                        && ps.ValidTo == model.ValidTo
                                        && ps.SubscriptionId == model.SubscriptionId));
                            }
                        }
                    }));
        }

        [Fact]
        public void CreatePaySubscription_WithDataInTheDb_ShouldReturnBadRequest()
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var userId = PaySubscriptionTestsData.UserId;
            var model = PaySubscriptionTestsData.CreateEmptyModel;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithData(users)
                .WithData(paySubscriptions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.SubscriptionId))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The SubscriptionId field is required")).Any());
                    }
                    
                    /*if (model.ValidTo == default(DateTime))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The ValidTo field is required")).Any());
                    }

                    if (model.ValidFrom == default(DateTime))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The ValidFrom field is required")).Any());
                    }*/
                }));

            Assert.False(From.Services<ApplicationDbContext>().PaySubscriptions
                                    .Any(ps => ps.SubscriptionId == model.SubscriptionId
                                        && ps.UserId == userId
                                        && ps.ValidTo == model.ValidTo
                                        && ps.ValidFrom == model.ValidFrom));
        }

        [Fact]
        public void EditPaysubscription_WithDataInTheDb_ShouldReturnOk()
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var userId = PaySubscriptionTestsData.UserId;
            var model = PaySubscriptionTestsData.UpdateModel;
            var paySubscriptionId = paySubscriptions[0].Id;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithData(users)
                .WithData(paySubscriptions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(paySubscriptionId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().PaySubscriptions
             .Any(m => m.Id == paySubscriptionId
                 && m.ModifiedOn != null));
        }

        [Fact]
        public void EditPaysubscription_WithEmptyModel_ShouldReturnBadRequest()
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var userId = PaySubscriptionTestsData.UserId;
            var model = PaySubscriptionTestsData.UpdateEmptyModel;
            var paySubscriptionId = PaySubscriptionTestsData.PaySubscriptions()[0].Id;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(users)
                .WithData(paySubscriptions)
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(paySubscriptionId, model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.SubscriptionId))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The SubscriptionId field is required")).Any());
                    }

                    /*if (model.ValidTo == default(DateTime))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The ValidTo field is required")).Any());
                    }
                    if (model.ValidFrom == default(DateTime))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The ValidFrom field is required")).Any());
                    }*/
                }));

            Assert.True(From.Services<ApplicationDbContext>().PaySubscriptions
                .Any(m => m.Id == paySubscriptionId
                    && m.ModifiedOn != null));
        }

        [Fact]
        public void EditPaysubscription_NotOwn_ShouldReturnForbid()
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var paySubscriptionId = PaySubscriptionTestsData.PaySubscriptions()[0].Id;
            var paySubscriptionOwnerUserId = PaySubscriptionTestsData.PaySubscriptions()[0].UserId;
            var userId = users.Where(u => u.Id!= paySubscriptionOwnerUserId).FirstOrDefault().Id;
            var model = PaySubscriptionTestsData.UpdateModel;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(users)
                .WithData(paySubscriptions)
                .WithData(subscriptions)
                .WithData(subscriptionClaims)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(paySubscriptionId, model))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().PaySubscriptions
                .Any(m => m.Id == paySubscriptionId
                    && m.ModifiedOn != null));
        }

        [Fact]
        public void GetById_WithDataInTheDb_ShouldReturnOk()
        {
            var paySubscriptions = PaySubscriptionTestsData.PaySubscriptions();
            var subscriptionClaims = PaySubscriptionTestsData.SubscriptionClaims;
            var subscriptions = PaySubscriptionTestsData.Subscriptions;
            var users = PaySubscriptionTestsData.Users;
            var userId = PaySubscriptionTestsData.UserId;
            var paySubscriptionId = paySubscriptions[0].Id;
            MyController<PaySubscriptionsController>
                .Instance()
                .WithData(paySubscriptions)
                .WithData(subscriptionClaims)
                .WithData(subscriptions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetById(paySubscriptionId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PaySubscriptionDetailsModel>()
                    .Passing(actual =>
                    {
                        var service = new PaySubscriptionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<PaySubscriptionDetailsModel>(paySubscriptionId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }
    }
}
