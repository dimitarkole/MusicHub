using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;

namespace MusicHub.Web.ViewModels.NotificationModels
{
    public class NotificationClaimInputModel : IMapTo<NotificationClaim>
    {
        public NotificationClaimType Type { get; set; }

        public string Content { get; set; }
    }
}