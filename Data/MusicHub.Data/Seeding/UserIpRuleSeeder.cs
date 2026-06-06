namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;

    using static MusicHub.Common.GlobalConstants;

    public class UserIpRuleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UserIpRules.Any())
            {
                return;
            }

            var random = new Random(0);

            var users = dbContext.Users
                .Where(u => u.Roles.Any(r => r.Role.Name == Roles.User))
                .ToList();

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

                    await dbContext.UserIpRules.AddAsync(rule);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
