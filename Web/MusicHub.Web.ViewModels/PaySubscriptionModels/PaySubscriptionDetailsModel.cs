using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using MusicHub.Web.ViewModels.SubscriptionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.PaySubscriptionModels
{
    public class PaySubscriptionDetailsModel : IMapTo<PaySubscription>
    {
        public string SubscriptionId { get; set; }

        public virtual PaySubscriptionDetailsModel Subscription { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
