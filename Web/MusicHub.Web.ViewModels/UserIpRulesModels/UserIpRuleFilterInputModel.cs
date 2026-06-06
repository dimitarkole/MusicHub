namespace MusicHub.Web.ViewModels.UserIpRulesModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Models.Enums;

    public class UserIpRuleFilterInputModel
    {
        public string Pattern { get; set; }

        public IpRuleType? RuleType { get; set; }

        public bool? IsEnabled { get; set; }

        public OrderMethod OrderMethod { get; set; } = OrderMethod.CreatedOnDesc;
    }
}
