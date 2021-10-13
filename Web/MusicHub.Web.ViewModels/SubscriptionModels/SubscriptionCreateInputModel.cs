using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;
using MusicHub.Web.ViewModels.SubscriptionClaimModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicHub.Web.ViewModels.SubscriptionModels
{
    public class SubscriptionCreateInputModel : IMapTo<Subscription>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public SubscriptionType Type { get; set; }

        [NotMapped]
        public ICollection<SubscriptionClaimCreateInputModel> SubcriptionClaims { get; set; }
    }
}
