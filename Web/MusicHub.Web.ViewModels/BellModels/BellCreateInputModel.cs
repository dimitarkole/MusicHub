namespace MusicHub.Web.ViewModels.BellModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class BellCreateInputModel : IMapTo<Bell>
    {
        [Required]
        public virtual string BellingId { get; set; }
    }
}
