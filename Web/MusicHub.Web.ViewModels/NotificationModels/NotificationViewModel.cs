using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.NotificationModels
{
    public class NotificationViewModel : IMapFrom<Notification>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public bool Seen { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
