using MusicHub.Common;
using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.SubscriptionModels
{
    public class SubscriptionFilter
    {
        public string Name { get; set; }

        public SubscriptionType Type { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
