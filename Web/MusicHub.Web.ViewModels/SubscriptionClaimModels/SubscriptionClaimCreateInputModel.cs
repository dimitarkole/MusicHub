using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Web.ViewModels.SubscriptionClaimModels
{
    public class SubscriptionClaimCreateInputModel : IMapTo<SubscriptionClaim>
    {
        [Required]
        public string Description { get; set; }
    }
}
