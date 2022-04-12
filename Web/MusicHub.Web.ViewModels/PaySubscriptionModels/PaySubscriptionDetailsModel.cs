namespace MusicHub.Web.ViewModels.PaySubscriptionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.SubscriptionModels;

    public class PaySubscriptionDetailsModel : IMapFrom<PaySubscription>
    {
        public string SubscriptionId { get; set; }

        public SubscriptionViewModel Subscription { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
