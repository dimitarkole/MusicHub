namespace MusicHub.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "MusicHub";

        public const string AdministratorRoleName = "Administrator";

        public const string CurrentCultureInfo = "bg-BG";
        public const string EnglishCultureInfo = "en-GB";

        public static class Roles
        {
            public const string SuperAdministrator = "SuperAdmin";
            public const string Administrator = "Admin";
            public const string User = "User";
            public const string AdministratorAndSuperAdministratorRoles = "Admin, SuperAdmin";
            public const string DataAnalisator = "DataAnalisator";
        }

        public static class PaginationData
        {
            public const int MusicsPerPage = 12;
            public const int SongsViewHistoryPerPage = 12;
            public const int CommentsPerPage = 12;
            public const int PlaylistsPerPage = 12;
            public const int PlaylistsSongsPerPage = 12;
            public const int LikedSongsPerPage = 12;
            public const int LicensesPerPage = 12;
        }
    }
}
