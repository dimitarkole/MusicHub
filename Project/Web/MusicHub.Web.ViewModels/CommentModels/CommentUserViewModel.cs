namespace MusicHub.Web.ViewModels.CommentModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class CommentUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string UserName { get; set; }
    }
}
