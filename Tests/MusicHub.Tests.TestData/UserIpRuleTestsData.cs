using Microsoft.EntityFrameworkCore;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Web.ViewModels.RoleModels;
using MusicHub.Web.ViewModels.UserIpRulesModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicHub.Tests.TestData
{
    public static class UserIpRuleTestsData
    {
        public static UserIpRuleCreateInputModel CreateModel => new UserIpRuleCreateInputModel()
        {
            Description = "test",
            IsEnabled = true,
            Pattern = "192.168.16.*",
            RuleType = IpRuleType.Allow,
        };

        public static UserIpRuleEditInputModel UpdateModel => new UserIpRuleEditInputModel()
        {
            Description = "test",
            IsEnabled = true,
            Pattern = "192.168.16.*",
            RuleType = IpRuleType.Allow,
        };

        public static string CreateUserId => Users[0].Id;

        public static List<User> Users => UserTestsData.Users;

        public static UserIpRuleFilterInputModel FilterModel => new UserIpRuleFilterInputModel() {
            IsEnabled = true,
            Pattern = "192.168.16.*",
            RuleType = IpRuleType.Allow,
        };

        public static List<UserIpRule> GenUserIpRules()
        {
            var random = new Random(0);
            var result = new List<UserIpRule>();
            var users = Users;
            var patterns = new List<string>()
            {
                "192.168.1.*",
                "192.168.0.*",
                "10.0.*",
                "172.16.*",
                "85.130.*",
                "95.111.*",
                "127.0.0.1",
                "::1",
                "192.168.1.10",
                "192.168.1.15",
                "192.168.2.*",
                "10.10.10.*",
            };

            var ruleTypes = Enum
                .GetValues(typeof(IpRuleType))
                .Cast<IpRuleType>()
                .Where(r => r != IpRuleType.All)
                .ToArray();
            foreach (var user in users)
            {
                var ruleCount = random.Next(1, 6);
                var availablePatterns = new List<string>(patterns);
                for (int i = 0; i < ruleCount && availablePatterns.Any(); i++)
                {
                    var pattern = availablePatterns[random.Next(availablePatterns.Count)];
                    availablePatterns.Remove(pattern);

                    var rule = new UserIpRule()
                    {
                        UserId = user.Id,
                        Pattern = pattern,
                        RuleType = (IpRuleType)ruleTypes.GetValue(random.Next(ruleTypes.Length)),
                        Description = $"Seeded rule for {pattern}",
                        IsEnabled = random.Next(0, 100) > 20,
                    };

                    result.Add(rule);
                }
            }

            return result;
        }
    }
}
