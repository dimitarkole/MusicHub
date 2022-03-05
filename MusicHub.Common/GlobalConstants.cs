namespace MusicHub.Common
{
    public static class GlobalConstants
    {
        public const string OctetStreamMimeType = "application/octet-stream";
        public const string SystemName = "MusicHub";

        public static class Email
        {
            public const string SystemEmail = "office.musichub@gmail.com";
            public const string ApiKey = "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";
        }

        public static class URLs
        {
            public const string SiteUrl = "www.musichub.com";
            public const string PrivacyUrl = "www.musichub.com";
            public const string Twitter = "www.musichub.com";
            public const string FAQ = "www.musichub.com";
            public const string Blog = "www.musichub.com";
            public const string NotificationSettings = "www.musichub.com";
            public const string ConfirmEmail = "https://en.MusicHub.com/confirm-email";
        }

        public static class MessagesStyle
        {
            public static string BackgroundHeaderStyle => "border-collapse:collapse;width:700px;height:35px;text-align:left;background-color: #3b125d;background-image: linear-gradient(to right, #2457d7, #3b125d);background: #3b125d;background: linear-gradient(to right, #2457d7, #3b125d);z-index: 100;";

            public static string LogoStyle => "width:651px;height:35px; line-height: 0.75;font-size: 30px;font-weight: 600;color: #ffffff;margin-left: 3px;transform: translateY(4px);font-family: 'Josefin Sans', sans-serif;";

            public static string FooterTextStyle => "color:#ffffff;font-family:Verdana, sans-serif;font-size:12px;font-weight:bold;text-decoration:none;";
        }

        public const string AdministratorRoleName = "Administrator";

        public const string CurrentCultureInfo = "bg-BG";
        public const string EnglishCultureInfo = "en-GB";

        public static class EmailSubjects
        {
            public const string ConfirmEmail = "Please confirm your email address";
            public const string ChangePassword = "Change password";
            public const string SuccessfulChangedPassword = "Successful changed cassword";
        }

        public static class Roles
        {
            public const string SuperAdministrator = "SuperAdmin";
            public const string Administrator = "Admin";
            public const string DataAnalisator = "DataAnalisator";
            public const string User = "User";
        }

        public static class PaginationData
        {
            public const int CategoriesPerPage = 10;
            public const int MusicsPerPage = 12;
            public const int SubscriptionsPerPage = 10;
            public const int RolesPerPage = 10;
            public const int PermissionsPerPage = 10;
            public const int PlayedMusicsPerPage = 12;
            public const int NotificationPerPage = 10;
            public const int CommentsPerPage = 12;
            public const int PlaylistsPerPage = 12;
            public const int AlbumsPerPage = 12;
            public const int AlbumMusicsPerPage = 12;
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
            public const string Email = "office.musichub@gmail.com";
            public const string Password = "Noskad5AKo!@";
        }

        public static class CreateFile
        {
            public const string RandomNameCharacters = "abcdefghijklmnopqrstuvwxyABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            public const int RandomNameLength = 5;
        }
    }
}
