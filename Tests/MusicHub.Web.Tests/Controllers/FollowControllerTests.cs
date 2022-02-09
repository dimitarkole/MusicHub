using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.FollowModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class FollowControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetOwnFollowing_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;

            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwnFollowing(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<FollowingsAllViewModel<FollowingViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.FollowingsPerPage;
                        var service = new FollowService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllFollowing<FollowingViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetFollowing_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;

            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .Calling(c => c.GetFollowing(userId, page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<FollowingsAllViewModel<FollowingViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.FollowingsPerPage;
                        var service = new FollowService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllFollowing<FollowingViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetFollowers_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;

            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .Calling(c => c.GetFollowers(userId, page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType< FollowersAllViewModel<FollowerViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.FollowersPerPage;
                        var service = new FollowService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllFollowers<FollowerViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterFollowers_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;
            var filter = FollowTestsData.FilterModelFirstName;
            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .Calling(c => c.FilterFollowers(userId, page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<FollowersAllViewModel<FollowerViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.FollowersPerPage;
                        var service = new FollowService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.FilterFollowers<FollowerViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterFollowings_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;
            var filter = FollowTestsData.FilterModelFirstName;

            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .Calling(c => c.FilterFollowings(userId, page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<FollowingsAllViewModel<FollowingViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.FollowingsPerPage;
                        var service = new FollowService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.FilterFollowings<FollowingViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void Create_WhitCurrectData_ShouldReturnOk()
        {
            var users = FollowTestsData.Users;
            var model = new FollowCreateInputModel()
            {
                FollowingId = users[1].Id,
            };
            var userId = users[0].Id;

            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .StatusCode(StatusCodes.Status201Created);

            Assert.True(From.Services<ApplicationDbContext>().Follows
                             .Any(f => f.FollowerId == userId
                                && f.FollowingId == model.FollowingId));
        }

        [Fact]
        public void Create_WhitCurrectData_UserFollowThem_ShouldReturnBadRequest()
        {
            var users = FollowTestsData.Users;
            var model = new FollowCreateInputModel()
            {
                FollowingId = users[0].Id,
            };
            var userId = users[0].Id;

            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .BadRequest();

            Assert.False(From.Services<ApplicationDbContext>().Follows
                             .Any(f => f.FollowerId == userId
                                && f.FollowingId == model.FollowingId));
        }

        [Fact]
        public void Create_WhitCurrectData_UserFollowUserАgain_ShouldReturnBadRequest()
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var model = new FollowCreateInputModel()
            {
                FollowingId = follows[0].FollowingId,
            };
            var userId = users[0].Id;

            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .BadRequest();

            Assert.Equal(1, From.Services<ApplicationDbContext>().Follows
                             .Count(f => f.FollowerId == userId
                                && f.FollowingId == model.FollowingId));
        }

        [Fact]
        public void IsFollowing_WhitCurrectData_ShouldReturnFalse()
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;
            var followingId = follows
                .Where(f => f.FollowerId != userId)
                .FirstOrDefault()
                .FollowingId;
            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsFollowing(followingId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.False(actual);
                    }));
        }

        [Fact]
        public void IsFollowing_WhitCurrectData_ShouldReturnTrue()
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;
            var followingId = follows
                .Where(f => f.FollowerId == userId)
                .FirstOrDefault()
                .FollowingId;
            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsFollowing(followingId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.True(actual);
                    }));
        }

        [Fact]
        public void GetFollowingId_WhitCurrectData_ShouldReturnNull()
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;

            var followingId = follows
                .Where(f => f.FollowerId != userId)
               . FirstOrDefault()
                .FollowingId;
            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetFollowingId(followingId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            if(property.Name == "Id")
                            {
                                Assert.NotEmpty(property.GetValue(obj).ToString());
                            }
                        }
                    }));
        }

        [Fact]
        public void GetFollowingId_WhitCurrectData_ShouldReturnOkWithFollowId()
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;
            var follow = follows
               .Where(f => f.FollowerId == userId)
               .FirstOrDefault();
            var followingId = follow.FollowingId;
            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetFollowingId(followingId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            if (property.Name == "Id")
                            {
                                Assert.Equal(follow.Id, property.GetValue(obj).ToString());
                            }
                        }
                    }));
        }

        [Fact]
        public void DeleteFollow_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;
            var deleteFollowId = follows.Where(f => f.FollowerId == userId)
                .FirstOrDefault()
                .Id;
            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteFollowId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Follows
                .Any(f => f.Id == deleteFollowId));
        }

        [Fact]
        public void DeleteNotOwnedFollow_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var users = FollowTestsData.Users;
            var follows = FollowTestsData.Follows();
            var userId = users[0].Id;
            var deleteFollowId = follows.Where(f => f.FollowerId != userId)
                .FirstOrDefault()
                .Id;
            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithData(follows)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteFollowId))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().Follows
                .Any(f => f.Id == deleteFollowId));
        }
    }
}
