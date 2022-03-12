namespace MusicHub.Web.ViewModels.FollowModels
{
    using MusicHub.Common;

    public class FollowFilterInputModel
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
