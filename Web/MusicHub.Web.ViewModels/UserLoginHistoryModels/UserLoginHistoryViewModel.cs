namespace MusicHub.Web.ViewModels.UserLoginHistoryModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class UserLoginHistoryViewModel : IMapFrom<UserLoginHistory>
    {
        public string IpAddress { get; set; }

        public DateTime LoginDate { get; set; }

        public string? UserAgent { get; set; }
    }
}
