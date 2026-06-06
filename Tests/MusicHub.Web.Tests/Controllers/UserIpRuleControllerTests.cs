using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.UserIpRulesModels;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class UserIpRuleControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterUserIpRules_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var rules = UserIpRuleTestsData.GenUserIpRules();
            var userId = UserIpRuleTestsData.CreateUserId;
            var filter = UserIpRuleTestsData.FilterModel;

            MyController<UserIpRuleController>
                .Instance()
                .WithData(rules)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(filter, page, PaginationData.UserIpRulesPerPage))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserIpRulePageModel<UserIpRuleViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new UserIpRuleService(
                            From.Services<ApplicationDbContext>(),
                            From.Services<IPaginationService>());

                        var expected = service.Filter<UserIpRuleViewModel>(
                            page,
                            PaginationData.UserIpRulesPerPage,
                            filter,
                            userId);

                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreateUserIpRule_WithValidData_ShouldReturnOkAndCreateRule()
        {
            var model = UserIpRuleTestsData.CreateModel;
            var userId = UserIpRuleTestsData.CreateUserId;

            MyController<UserIpRuleController>
                .Instance()
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Create(model))
                .ShouldReturn()
                .StatusCode(result => result
                    .WithStatusCode(StatusCodes.Status201Created)
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            if (property.Name == "userIpRuleId")
                            {
                                Assert.True(From.Services<ApplicationDbContext>().UserIpRules
                                    .Any(r => r.Id == property.GetValue(obj).ToString()
                                        && r.Description == model.Description
                                        && r.IsEnabled == model.IsEnabled
                                        && r.RuleType == model.RuleType
                                        && r.Pattern == model.Pattern));
                            }
                        }
                    }));
        }

        [Fact]
        public void UpdateUserIpRule_WithValidOwner_ShouldUpdateCorrectly()
        {
            var rules = UserIpRuleTestsData.GenUserIpRules();
            var model = UserIpRuleTestsData.UpdateModel;
            var userId = UserIpRuleTestsData.CreateUserId;
            var ruleId = rules.Where(r => r.UserId == userId).First().Id;

            MyController<UserIpRuleController>
                .Instance()
                .WithData(rules)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Update(ruleId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().UserIpRules
             .Any(r => r.Id == ruleId && r.ModifiedOn != null));
        }
    }
}
