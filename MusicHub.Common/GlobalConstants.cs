namespace MusicHub.Common
{
    public static class GlobalConstants
    {
        public const string OctetStreamMimeType = "application/octet-stream";
        public const string SystemName = "MusicHub";

        public const string AdministratorRoleName = "Administrator";

        public const string CurrentCultureInfo = "bg-BG";
        public const string EnglishCultureInfo = "en-GB";

        public static class Roles
        {
            public const string SuperAdministrator = "SuperAdmin";
            public const string Administrator = "Admin";
            public const string DataAnalisator = "DataAnalisator";
            public const string TeamAdministrator = "TeamAdmin";
            public const string Artist = "Artist";
            public const string User = "User";
        }

        public static class PaginationData
        {
            public const int MusicsPerPage = 12;
            public const int PlayedMusicsPerPage = 12;
            public const int NotificationPerPage = 10;
            public const int CommentsPerPage = 12;
            public const int PlaylistsPerPage = 12;
            public const int PlaylistsMusicsPerPage = 12;
            public const int LikedMusicsPerPage = 12;
            public const int LicensesPerPage = 12;
            public const int SuggestMusicPerPage = 10;
            public const int UsersPerPage = 10;
            public const int FollowingsPerPage = 10;
            public const int FollowersPerPage = 10;
        }

        public static class MegaStorageData
        {
            public static string Email { get; set; }

            public static string Password { get; set; }
        }
    }
}
