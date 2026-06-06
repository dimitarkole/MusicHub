using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Web.ViewModels.UserLoginHistoryModels
{
    public class UserLoginHistoryCreateInputModel : IMapTo<UserLoginHistory>
    {
        [Required]
        public string IpAddress { get; set; }

        [Required]
        public DateTime LoginDate { get; set; }

        public string? UserAgent { get; set; }
    }
}
