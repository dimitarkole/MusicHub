namespace MusicHub.Web.ViewModels.UserIpRulesModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class UserIpRuleEditInputModel : IMapTo<UserIpRule>
    {
        [Required]
        [Display(Name = "IP Address / Pattern")]
        [StringLength(100)]
        public string Pattern { get; set; }

        [Required]
        [Display(Name = "Rule Type")]
        public IpRuleType RuleType { get; set; }

        [Display(Name = "Description")]
        [StringLength(250)]
        public string Description { get; set; }

        [Display(Name = "Enabled")]
        public bool IsEnabled { get; set; } = true;
    }
}
