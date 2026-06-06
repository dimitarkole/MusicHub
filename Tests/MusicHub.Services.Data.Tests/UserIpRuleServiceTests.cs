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
        public async Task Create_WithValidData_ShouldWorkCorrectly()
        {
            var service = await this.CreateService(new List<UserIpRule>());
            var model = UserIpRuleTestsData.CreateModel;
            await service.Create(model, UserIpRuleTestsData.CreateUserId);
            Assert.True(this.context.UserIpRules.Any(r => r.Pattern == model.Pattern
                && r.Description == model.Description
                && r.IsEnabled == model.IsEnabled
                && r.RuleType == model.RuleType
                && r.Pattern == model.Pattern));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterUserIpRules_WithValidData_ShouldReturnFilteredRules(int page)
        {
            var userIpRules = UserIpRuleTestsData.GenUserIpRules();
            var service = await this.CreateService(userIpRules);
            var entitesPerPage = PaginationData.UserIpRulesPerPage;
            var filter = UserIpRuleTestsData.FilterModel;
            var actual = service.Filter<UserIpRuleViewModel>(
                page,
                entitesPerPage,
                filter,
                userId: UserIpRuleTestsData.CreateUserId);

            this.CheckFilter(actual, page, service, entitesPerPage, filter);
        }

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
        public async Task DeleteMusic_WithValidData_ShouldWorkCorrect()
        {
            var service = await this.CreateService(UserIpRuleTestsData.GenUserIpRules());
            var model = UserIpRuleTestsData.UpdateModel;
            var ruleId = this.context.UserIpRules.FirstOrDefault().Id;

            await service.Delete(ruleId);

            Assert.False(this.context.UserIpRules.Any(c => c.Id == ruleId));
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var service = await this.CreateService(UserIpRuleTestsData.GenUserIpRules());
            var model = UserIpRuleTestsData.UpdateModel;
            var ruleId = this.context.UserIpRules.FirstOrDefault().Id;

            var result = service.GetById<UserIpRuleViewModel>(ruleId);

            Assert.True(this.context.UserIpRules.Any(r => r.Id == ruleId
                  && r.RuleType == result.RuleType
                  && r.Pattern == result.Pattern
                  && r.Description == result.Description
                  && r.CreatedOn == result.CreatedOn));
        }

        [Fact]
        public async Task IsOwned_WithValidData_ShouldWorkCorrect_ReturnsTrue()
        {
            var service = await this.CreateService(UserIpRuleTestsData.GenUserIpRules());
            var model = UserIpRuleTestsData.UpdateModel;
            var rule = this.context.UserIpRules.FirstOrDefault();
            var result = service.IsOwned(rule.Id, rule.UserId);
            Assert.True(result);
        }

        [Fact]
        public async Task IsOwned_WithInvalidUser_ShouldReturnFalse()
        {
            var service = await this.CreateService(UserIpRuleTestsData.GenUserIpRules());
            var rule = this.context.UserIpRules.FirstOrDefault();
            var wrongUserId = Guid.NewGuid().ToString();
            var result = service.IsOwned(rule.Id, wrongUserId);
            Assert.False(result);
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

        [Fact]
        public async Task HasAccess_WithExactAllowRule_ShouldReturnTrue()
        {
            var userId = UserIpRuleTestsData.Users[0].Id;
            var rules = new List<UserIpRule>
            {
                new UserIpRule
                {
                    UserId = userId,
                    IsEnabled = true,
                    RuleType = IpRuleType.Allow,
                    Pattern = "192.168.1.50"
                }
            };

            var service = await this.CreateService(rules);
            var result = service.HasAccess(userId, "192.168.1.50");
            Assert.True(result);
        }

        [Fact]
        public async Task HasAccess_WithWildcardAllowRule_ShouldReturnTrue()
        {
            var userId = UserIpRuleTestsData.Users[0].Id;
            var rules = new List<UserIpRule>
            {
                new UserIpRule
                {
                    UserId = userId,
                    IsEnabled = true,
                    RuleType = IpRuleType.Allow,
                    Pattern = "192.168.1.*"
                }
            };

            var service = await this.CreateService(rules);

            var result = service.HasAccess(userId, "192.168.1.50");

            Assert.True(result);
        }

        private void CheckFilter(UserIpRulePageModel<UserIpRuleViewModel> actual, int page, UserIpRuleService service, int entitesPerPage, UserIpRuleFilterInputModel filter, string userId = null)
        {
            IQueryable<UserIpRule> rules = this.context.UserIpRules
                .Where(r => r.UserId == userId);
            rules = this.FilteringRules(filter, rules);
            var expected = new UserIpRulePageModel<UserIpRuleViewModel>()
            {
                UserIpRules = rules
                    .GetPage(page, entitesPerPage)
                    .To<UserIpRuleViewModel>()
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(
                    rules.Count(),
                    entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        private IQueryable<UserIpRule> FilteringRules(UserIpRuleFilterInputModel filter, IQueryable<UserIpRule> rules)
        {
            if (!string.IsNullOrWhiteSpace(filter.Pattern))
            {
                rules = rules.Where(r =>
                    r.Pattern.ToLower().Contains(filter.Pattern.ToLower()));
            }

            if (filter.RuleType != null && filter.RuleType != 0)
            {
                rules = rules.Where(r => r.RuleType == filter.RuleType);
            }

            if (filter.IsEnabled != null)
            {
                rules = rules.Where(r => r.IsEnabled == filter.IsEnabled);
            }

            return rules;
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
