using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.VerificationCodeModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Services.Mapping;

namespace MusicHub.Web.Tests.Controllers
{
    public class VerificationControllerTests
    {
        [Fact]
        public void GetVerificationById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = VerificationCodeTestsData.Users;
            var userId = VerificationCodeTestsData.UserId;
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCodeId = verificationCodes[0].Id;
            MyController<VerificationController>
                .Instance()
                .WithData(users)
                .WithData(verificationCodes)
                .Calling(c => c.GetById(verificationCodeId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<VerificationCodeViewModel>()
                    .Passing(actual =>
                    {
                        var service = new VerificationCodeService(From.Services<ApplicationDbContext>());
                        var expected = service.GetById<VerificationCodeViewModel>(verificationCodeId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }


        [Fact]
        public void GetActivatedVerificationCodeByUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = VerificationCodeTestsData.Users;
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCode = verificationCodes.Where(vc => vc.IsUsed == false)
                .FirstOrDefault();
            var userId = verificationCode.UserId;
            var model = new VerificationCodeCheckInputModel()
            {
                Type = verificationCode.Type
            };

            MyController<VerificationController>
                .Instance()
                .WithData(users)
                .WithData(verificationCodes)
                .Calling(c => c.GetActivatedVerificationCode(userId, model))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<VerificationCodeViewModel>()
                    .Passing(actual =>
                    {
                        var service = new VerificationCodeService(From.Services<ApplicationDbContext>());
                        var expected = service.GetActivatedVerificationCode<VerificationCodeViewModel>(userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreateVerificationCode_WithDataInTheDb_ShouldReturnOk()
        {
            var model = VerificationCodeTestsData.CreateModel;
            var userId = VerificationCodeTestsData.UserId;
            MyController<VerificationController>
                .Instance()
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .StatusCode(result => result
                    .WithStatusCode(StatusCodes.Status201Created));

            Assert.True(From.Services<ApplicationDbContext>().VerificationCodes
                .Any(vc =>  vc.IsUsed == false 
                        && vc.Type == model.Type
                        && vc.UserId == userId
                        && vc.Code == model.Code));
        }

        [Fact]
        public void CheckCode_WithDataInTheDb_ShouldReturnOkWithTrue()
        {
            var service = new VerificationCodeService(From.Services<ApplicationDbContext>());
            var users = VerificationCodeTestsData.Users;
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var userId = verificationCodes
                .Where(vc => vc.IsUsed == false)
                .FirstOrDefault()
                .UserId;

            var verificationCode = verificationCodes
                .Where(vc => vc.UserId == userId
                  && vc.IsUsed == false)
                .OrderByDescending(vc => vc.CreatedOn)
                .To<VerificationCodeViewModel>()
                .FirstOrDefault();
            var model = new VerificationCodeCheckInputModel()
            {
                Type = verificationCode.Type ,
                Code = verificationCode.Code,
            };

            MyController<VerificationController>
                .Instance()
                .WithData(users)
                .WithData(verificationCodes)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.CheckCode(userId, model))
                 .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.True(actual);
                    }));
        }


        [Fact]
        public void CheckCode_CodeIsUsed_WithDataInTheDb_ShouldReturnOkWithFalse()
        {
            var service = new VerificationCodeService(From.Services<ApplicationDbContext>());
            var users = VerificationCodeTestsData.Users;
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var userId = verificationCodes
                .Where(vc => vc.IsUsed == true)
                .FirstOrDefault()
                .UserId;
            var verificationCode = verificationCodes
                .Where(vc => vc.UserId == userId
                  && vc.IsUsed == true)
                .OrderByDescending(vc => vc.CreatedOn)
                .To<VerificationCodeViewModel>()
                .FirstOrDefault();
            var model = new VerificationCodeCheckInputModel()
            {
                Type = verificationCode.Type,
                Code = verificationCode.Code,
            };

            MyController<VerificationController>
                .Instance()
                .WithData(users)
                .WithData(verificationCodes)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.CheckCode(userId, model))
                 .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.False(actual);
                    }));
        }

        [Fact]
        public void CheckCode_CodeIsIncurrenct_WithDataInTheDb_ShouldReturnOkWithFalse()
        {
            var service = new VerificationCodeService(From.Services<ApplicationDbContext>());
            var users = VerificationCodeTestsData.Users;
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var userId = verificationCodes
                .Where(vc => vc.IsUsed == false)
                .FirstOrDefault()
                .UserId;

            var verificationCode = verificationCodes
                .Where(vc => vc.UserId == userId
                  && vc.IsUsed == false)
                .OrderByDescending(vc => vc.CreatedOn)
                .To<VerificationCodeViewModel>()
                .FirstOrDefault();
            var model = new VerificationCodeCheckInputModel()
            {
                Type = verificationCode.Type,
                Code = verificationCode.Code + "Akodsa",
            };

            MyController<VerificationController>
                .Instance()
                .WithData(users)
                .WithData(verificationCodes)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.CheckCode(userId, model))
                 .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.False(actual);
                    }));
        }
    }
}
