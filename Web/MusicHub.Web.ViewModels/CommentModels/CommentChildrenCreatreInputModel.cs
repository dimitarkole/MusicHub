namespace MusicHub.Web.ViewModels.CommentModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class CommentChildrenCreatreInputModel : IMapTo<Comment>
    {
        [Required]
        public string Text { get; set; }
    }
}
