namespace MusicHub.Web.ViewModels.ProfilleModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    using System;

    public class ProfileDetailsViewModel : IMapFrom<ApplicationUser>
    {
        public virtual string Id { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public virtual int SongsCount { get; set; }

        public virtual int PlaylistsCount { get; set; }
    }
}
