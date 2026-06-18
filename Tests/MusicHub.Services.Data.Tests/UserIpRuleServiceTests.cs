namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;
    using Microsoft.Extensions.FileSystemGlobbing.Internal;
    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.UserIpRulesModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

    public class UserIpRuleServiceTests : BaseTestClass
    {
        [Fact]
        public async Task UpdateUserIpRule_WithValidData_ShouldUpdateCorrectly()
        {
            // Arrange
            var service = await this.CreateService(UserIpRuleTestsData.GenUserIpRules());
            var model = UserIpRuleTestsData.UpdateModel;
            var ruleId = this.context.UserIpRules.FirstOrDefault().Id;

            // Act
            await service.Update(model, ruleId);

            // Assert
            var updatedRule = this.context.UserIpRules
                .FirstOrDefault(r => r.Id == ruleId);

            Assert.NotNull(updatedRule);

            Assert.Equal(model.Pattern, updatedRule.Pattern);
            Assert.Equal(model.RuleType, updatedRule.RuleType);
            Assert.Equal(model.Description, updatedRule.Description);
            Assert.Equal(model.IsEnabled, updatedRule.IsEnabled);
        }

        [Fact]
        public async Task IsOwned_WithInvalidRuleId_ShouldReturnFalse()
        {
            var service = await this.CreateService(UserIpRuleTestsData.GenUserIpRules());
            var wrongRuleId = Guid.NewGuid().ToString();
            var userId = this.context.UserIpRules.First().UserId;
            var result = service.IsOwned(wrongRuleId, userId);

            Assert.False(result);
        }

        [Fact]
        public async Task HasAccess_WithNoRules_ShouldReturnTrue()
        {
            var service = await this.CreateService(UserIpRuleTestsData.GenUserIpRules());
            var result = service.HasAccess("any-user", "192.168.1.10");
            Assert.True(result);
        }

        [Fact]
        public async Task HasAccess_WithDenyRule_ShouldOverrideAllowAndReturnFalse()
        {
            var userIpRules = UserIpRuleTestsData.GenUserIpRules();
            var service = await this.CreateService(userIpRules);
            var ipDeny = userIpRules.Where(r => r.RuleType == IpRuleType.Deny)
                .FirstOrDefault();

            var result = service.HasAccess(ipDeny.UserId, ipDeny.Pattern);
            Assert.False(result);
        }

        [Fact]
        public async Task HasAccess_WithNoMatchingRule_ShouldReturnFalse()
        {
            var userIpRules = UserIpRuleTestsData.GenUserIpRules();
            var service = await this.CreateService(userIpRules);
            var userId = userIpRules.First().UserId;
            var result = service.HasAccess(userId, "255.255.255.255");
            Assert.False(result);
        }

        [Fact]
        public async Task HasAccess_WithWildcardDenyRule_ShouldReturnFalse()
        {
            var userIpRules = UserIpRuleTestsData.GenUserIpRules();
            var service = await this.CreateService(userIpRules);
            var denyRule = userIpRules
                .First(r => r.RuleType == IpRuleType.Deny
                            && r.Pattern.Contains("*"));
            var result = service.HasAccess(denyRule.UserId, "192.168.1.99");
            Assert.False(result);
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<UserIpRuleService> CreateService(List<UserIpRule> userIpRules)
        {
            await this.context.UserIpRules.AddRangeAsync(userIpRules);
            await this.context.Users.AddRangeAsync(UserIpRuleTestsData.Users);
            await this.context.SaveChangesAsync();
            return new UserIpRuleService(this.context, new PaginationService());
        }
    }
}
