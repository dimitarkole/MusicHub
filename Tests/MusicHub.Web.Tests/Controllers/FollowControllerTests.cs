using Microsoft.AspNetCore.Http;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.FollowModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MusicHub.Web.Tests.Controllers
{
    public class FollowControllerTests
    {
        [Fact]
        public void Create_WhitCurrectData_ShouldReturnOk()
        {
            var users = FollowTestsData.Users;
            var model = new FollowCreateInputModel()
            {
                FollowerId = users[0].Id,
            };

            MyController<FollowController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, "testUser"))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .StatusCode(StatusCodes.Status201Created);
        }
    }
}
