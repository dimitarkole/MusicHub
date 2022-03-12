using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.FollowModels;
using Microsoft.AspNetCore.Http;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MusicHub.Web.Tests.Controllers
{
    public class FollowControllerTest
    {
        [Fact]
        public void Create_WhitCurrectData_ShouldReturnOk()
            => MyController<FollowController>
            .Instance()
            .WithUser(user => user
                .WithClaim(ClaimTypes.NameIdentifier, "1"))
            .WithData(UserTestsData.Users)
            .Calling(c => c.Post(FollowTestsData.CreateModel))
            .ShouldReturn()
            .StatusCode(StatusCodes.Status201Created);

        [Fact]
        public void Create_WhitCurrectData_FollowerUserIdIsEqualsFollowingUserId_ShouldReturnBadRequest()
            => MyController<FollowController>
            .Instance()
            .WithUser(user => user
                .WithClaim(ClaimTypes.NameIdentifier, FollowTestsData.FollowerUserIdIsEqualsFollowingUserId))
            .WithData(UserTestsData.Users)
            .Calling(c => c.Post(FollowTestsData.CreateModel))
            .ShouldReturn()
            .BadRequest();

        [Fact]
        public void GetFollowing_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            IList<Follower> followers = FollowTestsData.GetFollows;
            var userId = FollowTestsData.FollowedUserId;
            MyController<FollowController>
            .Instance()
            .WithData(FollowTestsData.Users)
            .WithData(followers)
            .WithUser(user => user
                .WithClaim(ClaimTypes.NameIdentifier, userId))
            .Calling(c => c.GetFollowing())
            .ShouldReturn()
            .Ok(result => result
                .WithModelOfType<List<FollowViewModel>>()
                .Passing(actualFollowing =>
                {
                    followers = followers
                        .Where(f => f.FollowingId == userId)
                        .ToList();
                    Assert.Equal(followers.Count(), actualFollowing.Count());
                    for (int i = 0; i < followers.Count; i++)
                    {
                        Assert.Contains(actualFollowing, c => c.Id == followers[i].Id
                            && c.Followed.Id == userId);
                    }
                }));
        }

        [Fact]
        public void GetFollowers_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            IList<Follower> followers = FollowTestsData.GetFollows;
            var userId = FollowTestsData.FollowedUserId;
            MyController<FollowController>
            .Instance()
            .WithData(followers)
            .WithUser(user => user
                .WithClaim(ClaimTypes.NameIdentifier, userId))
            .Calling(c => c.GetFollowers())
            .ShouldReturn()
            .Ok(result => result
                .WithModelOfType<IList<FollowedViewModel>>()
                .Passing(actualFollowed =>
                {
                    followers = followers
                        .Where(f => f.FollowingId == userId)
                        .ToList();
                    Assert.Equal(followers.Count(), actualFollowed.Count());
                    for (int i = 0; i < followers.Count; i++)
                    {
                        Assert.Contains(actualFollowed, c => c.Id == followers[i].Id
                            && c.Following.Id == followers[i].FollowedId);
                    }
                }));
        }

        [Fact]
        public void GetFollowingByUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            IList<Follower> followers = FollowTestsData.GetFollows;
            var userId = FollowTestsData.FollowedUserId;
            MyController<FollowController>
            .Instance()
            .WithData(FollowTestsData.Users)
            .WithData(followers)
            .Calling(c => c.GetFollowing(userId))
            .ShouldReturn()
            .Ok(result => result
                .WithModelOfType<List<FollowViewModel>>()
                .Passing(actualFollowing =>
                {
                    followers = followers
                        .Where(f => f.FollowingId == userId)
                        .ToList();
                    Assert.Equal(followers.Count(), actualFollowing.Count());
                    for (int i = 0; i < followers.Count; i++)
                    {
                        Assert.Contains(actualFollowing, c => c.Id == followers[i].Id
                            && c.Followed.Id == userId);
                    }
                }));
        }

        [Fact]
        public void GetFollowersByUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            IList<Follower> followers = FollowTestsData.GetFollows;
            var userId = FollowTestsData.FollowedUserId;
            MyController<FollowController>
            .Instance()
            .WithData(followers)
            .Calling(c => c.GetFollowers(userId))
            .ShouldReturn()
            .Ok(result => result
                .WithModelOfType<IList<FollowedViewModel>>()
                .Passing(actualFollowed =>
                {
                    followers = followers
                        .Where(f => f.FollowingId == userId)
                        .ToList();
                    Assert.Equal(followers.Count(), actualFollowed.Count());
                    for (int i = 0; i < followers.Count; i++)
                    {
                        Assert.Contains(actualFollowed, c => c.Id == followers[i].Id
                            && c.Following.Id == followers[i].FollowedId);
                    }
                }));
        }

        [Fact]
        public void Delete_WithDataInTheDb_ShouldReturnOk()
        {
            IList<Follower> followers = FollowTestsData.GetFollows;
            var deleteFollowId = FollowTestsData.DeleteFollowId;

            MyController<FollowController>
                .Instance()
                .WithData(followers)
                .Calling(c => c.Delete(deleteFollowId))
                .ShouldReturn()
                .Ok();
        }


        [Fact]
        public void GetFollowerId_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            IList<Follower> followers = FollowTestsData.GetFollows;
            var userId = FollowTestsData.FollowedUserId; 
            var followedUserId = followers.Where(f => f.FollowedId == userId)
                .FirstOrDefault()
                .FollowingId;
            MyController<FollowController>
                .Instance()
                .WithData(FollowTestsData.Users)
                .WithData(followers)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetFollowerId(followedUserId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<string>()
                    .Passing(actualFollowing =>
                    {
                        var expectedResult = followers
                            .Where(f => f.FollowedId == userId
                                && f.FollowingId == followedUserId)
                            .FirstOrDefault()
                            .Id;
                        Assert.Equal(expectedResult, actualFollowing);
                    }));
        }

        [Fact]
        public void GetFollowId_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            IList<Follower> followers = FollowTestsData.GetFollows;
            var followedUserId = FollowTestsData.FollowedUserId;
            var userId = followers.Where(f => f.FollowedId == followedUserId)
                .FirstOrDefault()
                .FollowingId;
            MyController<FollowController>
                .Instance()
                .WithData(FollowTestsData.Users)
                .WithData(followers)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetFollowId(followedUserId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<string>()
                    .Passing(actualFollowing =>
                    {
                        Assert.Equal(followers.Where(f =>
                            f.FollowedId == followedUserId
                            && f.FollowingId == userId)
                                .FirstOrDefault()
                                .Id, actualFollowing);
                    }));
        }

        [Fact]
        public void IsFollowed_WithDataInTheDb_ShouldReturnTrue()
        {
            IList<Follower> followers = FollowTestsData.GetFollows;
            var userId = FollowTestsData.FollowingUserIdIsFollowedTrue;
            var followedUserId = FollowTestsData.FollowedUserIdIsFollowedTrue;
            MyController<FollowController>
                .Instance()
                .WithData(FollowTestsData.Users)
                .WithData(followers)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsFollowed(followedUserId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actualFollowing =>
                    {
                        Assert.True(actualFollowing);
                    }));
        }

        [Fact]
        public void IsFollowed_WithDataInTheDb_ShouldReturnFalse()
        {
            IList<Follower> followers = FollowTestsData.GetFollows;
            var userId = FollowTestsData.FollowingUserIdIsFollowedFalse();
            var followedUserId = FollowTestsData.FollowedUserIdIsFollowedFalse;
            MyController<FollowController>
                .Instance()
                .WithData(FollowTestsData.Users)
                .WithData(followers)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsFollowed(followedUserId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actualFollowing =>
                    {
                        Assert.False(actualFollowing);
                    }));
        }
    }
}
