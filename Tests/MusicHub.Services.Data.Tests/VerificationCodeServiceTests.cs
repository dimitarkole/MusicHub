using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
using MusicHub.Web.ViewModels.VerificationCodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Services.Mapping;
using FluentAssertions;

namespace MusicHub.Services.Data.Tests
{
    public class VerificationCodeServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateVerificationCode_WithValidData_ShouldWorkCorrect()
        {
            var verificationCodeService = await this.CreateService(new List<VerificationCode>());
            var model = VerificationCodeTestsData.CreateModel;

            await verificationCodeService.Create(model);

            Assert.True(this.context.VerificationCodes.Any(vc => vc.UserId == model.UserId
                && vc.Type == model.Type
                && vc.Code == model.Code
                && vc.IsUsed == false));
        }

        [Fact]
        public async Task IsVerificationCodeCorrect_WithValidData_ShouldReturnTrue()
        {
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCodeService = await this.CreateService(verificationCodes);
            var userId = verificationCodes.Where(vc => vc.IsUsed == false)
                .FirstOrDefault()
                .UserId;
            var lastActiveVerificationCode = this.context.VerificationCodes
                .Where(vc => vc.UserId == userId && vc.IsUsed == false)
                .OrderByDescending(vc => vc.CreatedOn)
                .To<VerificationCodeViewModel>()
                .FirstOrDefault();
            var model = new VerificationCodeCheckInputModel()
            {
                Code = lastActiveVerificationCode.Code,
                Type = lastActiveVerificationCode.Type,
            };

            var result = verificationCodeService.IsVerificationCodeCorrect(userId, model);

            Assert.True(result);
        }

        [Fact]
        public async Task IsVerificationCodeCorrect_CodeIsNotOwn_WithValidData_ShouldReturnFalse()
        {
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCodeService = await this.CreateService(verificationCodes);
            var userId = verificationCodes[0].UserId;
            var lastActiveVerificationCode = this.context.VerificationCodes
                .Where(vc => !(vc.UserId == userId) && vc.IsUsed == false)
                .OrderByDescending(vc => vc.CreatedOn)
                .To<VerificationCodeViewModel>()
                .FirstOrDefault();
            var model = new VerificationCodeCheckInputModel()
            {
                Code = lastActiveVerificationCode.Code,
                Type = lastActiveVerificationCode.Type,
            };

            var result = verificationCodeService.IsVerificationCodeCorrect(userId, model);

            Assert.False(result);
        }

        [Fact]
        public async Task GetActivatedVerificationCode_WithValidData_ShouldReturnData()
        {
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCodeService = await this.CreateService(verificationCodes);
            var userId = verificationCodes[0].UserId;

            var actual = verificationCodeService.GetActivatedVerificationCode<VerificationCodeViewModel>(userId);
            var expected = this.context.VerificationCodes
                .Where(vc => vc.UserId == userId && vc.IsUsed == false)
                .OrderByDescending(vc => vc.CreatedOn)
                .To<VerificationCodeViewModel>()
                .FirstOrDefault();

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task GetActivatedVerificationCodeWithVerificationType_WithValidData_ShouldReturnData()
        {
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCodeService = await this.CreateService(verificationCodes);
            var userId = verificationCodes[0].UserId;
            var type = Common.VerificationType.ChangePassword;
            var actual = verificationCodeService.GetActivatedVerificationCode<VerificationCodeViewModel>(userId, type);
            var expected = this.context.VerificationCodes
                .Where(vc => vc.UserId == userId
                    && vc.Type == type
                    && vc.IsUsed == false)
                .OrderByDescending(vc => vc.CreatedOn)
                .To<VerificationCodeViewModel>()
                .FirstOrDefault();

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldReturnData()
        {
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCodeService = await this.CreateService(verificationCodes);
            var varificationId = verificationCodes[0].UserId;
            var actual = verificationCodeService.GetById<VerificationCodeViewModel>(varificationId);
            var expected = this.context.VerificationCodes
                .Where(vc => vc.Id == varificationId)
                .To<VerificationCodeViewModel>()
                .FirstOrDefault();

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task SetVerificationCodeIsUsed_WithValidData_ShouldReturnData()
        {
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCodeService = await this.CreateService(verificationCodes);
            var userId = verificationCodes.Where(vc => vc.IsUsed == false)
                .FirstOrDefault()
                .UserId;
            var verificationCode = verificationCodes
                .Where(vc => vc.UserId == userId
                    && vc.IsUsed == false)
                .OrderByDescending(vc => vc.CreatedOn)
                .FirstOrDefault()
                .Code;

            await verificationCodeService.SetVerificationCodeIsUsed(userId, verificationCode);

            Assert.True(this.context.VerificationCodes.Any(vc => vc.UserId == userId
                && vc.IsUsed == true && vc.Code == verificationCode));
        }

        [Fact]
        public async Task DeleteVerificationCode_WithValidData_ShouldWorkCorrect()
        {
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCodeService = await this.CreateService(verificationCodes);
            var verificationCodeId = verificationCodes[0].Id;
            await verificationCodeService.Delete(verificationCodeId);

            Assert.False(this.context.VerificationCodes.Any(c => c.Id == verificationCodeId));
        }

        private async Task<VerificationCodeService> CreateService(List<VerificationCode> verificationCodes)
        {
            var users = VerificationCodeTestsData.Users;
            await this.context.Users.AddRangeAsync(users);
            await this.context.VerificationCodes.AddRangeAsync(verificationCodes);
            await this.context.SaveChangesAsync();
            return new VerificationCodeService(this.context, new PaginationService());
        }
    }
}
