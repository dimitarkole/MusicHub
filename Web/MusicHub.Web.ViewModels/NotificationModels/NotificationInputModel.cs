using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.NotificationModels
{
    public class NotificationInputModel : IMapTo<Notification>
    {
        public NotificationInputModel(string content, string userId)
        {
            Content = content;
            UserId = userId;
        }

        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
