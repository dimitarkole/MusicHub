namespace MusicHub.Web.ViewModels.UserIpRulesModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class UserIpRuleViewModel : IMapFrom<UserIpRule>
    {
        public string Id { get; set; } = string.Empty;

        public IpRuleType RuleType { get; set; }

        public string Pattern { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsEnabled { get; set; } = true;

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
