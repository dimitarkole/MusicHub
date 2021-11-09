using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.SubscriptionModels
{
    public class SubscriptionsAllViewModel<T>
    {
        public IEnumerable<T> Subscriptions { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
