namespace MusicHub.Web.ViewModels.CategoryModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using static MusicHub.Common.ModelConstants;

    public class CategoryCreateInputModel : IMapTo<Category>
    {
        [Required]
        [MinLength(CategotyConstants.NameMinLength)]
        [MaxLength(CategotyConstants.NameMaxLength)]
        public string Name { get; set; }
    }
}
