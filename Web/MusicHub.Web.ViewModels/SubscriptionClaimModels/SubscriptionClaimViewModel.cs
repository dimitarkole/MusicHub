using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.SubscriptionClaimModels
{
    public class SubscriptionClaimViewModel : IMapFrom<SubscriptionClaim>
    {
        public string Id { get; set; }

        public string Description { get; set; }
    }
}
