namespace MusicHub.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;

    public class UserFilter
    {
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
