using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;

namespace MusicHub.Web.ViewModels.NotificationModels
{
    public class NotificationClaimViewModel : IMapFrom<NotificationClaim>
    {
        public NotificationClaimType Type { get; set; }

        public string Content { get; set; }
    }
}