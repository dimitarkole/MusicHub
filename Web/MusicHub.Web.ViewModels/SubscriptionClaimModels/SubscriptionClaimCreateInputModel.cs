using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.SubscriptionClaimModels
{
    public class SubscriptionClaimCreateInputModel : IMapTo<SubscriptionClaim>
    {
        public string Description { get; set; }
    }
}
