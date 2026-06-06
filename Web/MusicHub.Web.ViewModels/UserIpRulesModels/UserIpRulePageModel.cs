namespace MusicHub.Web.ViewModels.UserIpRulesModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class UserIpRulePageModel<T> : PageModel
    {
        public IEnumerable<T> UserIpRules { get; set; }
    }
}
