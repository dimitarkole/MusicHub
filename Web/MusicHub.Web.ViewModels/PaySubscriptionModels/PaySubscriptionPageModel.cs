namespace MusicHub.Web.ViewModels.PaySubscriptionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class PaySubscriptionPageModel<T> : PageModel
    {
        public IEnumerable<T> PaySubscriptions { get; set; }
    }
}
