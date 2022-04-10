namespace MusicHub.Web.ViewModels.PaySubscriptionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;

    public class PaySubscriptionFilter
    {
        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
