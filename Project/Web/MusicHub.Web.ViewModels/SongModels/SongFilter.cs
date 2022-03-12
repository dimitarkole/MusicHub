namespace MusicHub.Web.ViewModels.SongModels
{
    using MusicHub.Common;

    public class SongFilter
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public string CategoryId { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
