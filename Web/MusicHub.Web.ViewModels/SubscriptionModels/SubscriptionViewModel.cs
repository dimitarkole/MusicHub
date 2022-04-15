using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;
using MusicHub.Web.ViewModels.SubscriptionClaimModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.SubscriptionModels
{
    public class SubscriptionViewModel : IMapFrom<Subscription>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public SubscriptionType Type { get; set; }

        public virtual ICollection<SubscriptionClaimViewModel> SubcriptionClaims { get; set; }
    }
}
