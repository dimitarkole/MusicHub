namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.UserIpRulesModels;

    public class UserIpRuleService : IUserIpRuleService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public UserIpRuleService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public UserIpRulePageModel<T> Filter<T>(int page, int entitesPerPage, UserIpRuleFilterInputModel model, string userId)
        {
            var rules = this.context.UserIpRules.Where(r => r.UserId == userId);
            rules = this.FilteringRules(model, rules);
            var rulesT = rules.To<T>();
            return new UserIpRulePageModel<T>()
            {
                UserIpRules = rulesT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(rules.Count(), entitesPerPage),
            };
        }

        public async Task<string> Create(UserIpRuleCreateInputModel model, string userId)
        {
            var rule = model.To<UserIpRule>();
            rule.UserId = userId;

            await this.context.UserIpRules.AddAsync(rule);
            await this.context.SaveChangesAsync();

            return rule.Id;
        }

        public async Task Update(UserIpRuleEditInputModel model, string id)
        {
            var rule = this.context.UserIpRules
                .FirstOrDefault(r => r.Id == id);

            model.To<UserIpRule>(rule);

            this.context.UserIpRules.Update(rule);

            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var rule = this.context.UserIpRules.Find(id);
            this.context.UserIpRules.Remove(rule);
            await this.context.SaveChangesAsync();
        }

        public T GetById<T>(string id)
            => this.context.UserIpRules
                .Where(r => r.Id == id)
                .To<T>()
                .FirstOrDefault();

        public bool IsOwned(string id, string userId)
            => this.context.UserIpRules
                .Any(r => r.Id == id && r.UserId == userId);

        private IQueryable<UserIpRule> FilteringRules(
            UserIpRuleFilterInputModel filter,
            IQueryable<UserIpRule> rules)
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
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
