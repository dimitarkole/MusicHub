namespace MusicHub.Web.ViewModels.PaySubscriptionModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PaySubscriptionEditModel : IMapTo<PaySubscription>
    {
        [Required]
        public string SubscriptionId { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }
    }
}
