namespace MusicHub.Web.ViewModels.PaySubscriptionModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PaySubscriptionViewModel : IMapFrom<PaySubscription>
    {
        public virtual string Id { get; set; }

        public string SubscriptionId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
