namespace MusicHub.Web.ViewModels.SubscriptionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class SubscriptionPageModel<T> : PageModel
    {
        public IEnumerable<T> Subscriptions { get; set; }
    }
}
