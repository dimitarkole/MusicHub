using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.Users
{
    public class ResendEmailConfirmDetailsModel : IMapFrom<User>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public string VerificationCode { get; set; }
    }
}
