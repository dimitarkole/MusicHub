namespace MusicHub.Web.ViewModels.PaySubscriptionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PaySubscriptionCreateInputModel : IMapTo<PaySubscription>
    {
        public string SubscriptionId { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
