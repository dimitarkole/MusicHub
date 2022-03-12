namespace MusicHub.Common
{
    public static class GlobalConstants
    {
        public const string OctetStreamMimeType = "application/octet-stream";
        public const string SystemName = "MusicHub";
        public const string CurrentCultureInfo = "bg-BG";
        public const string EnglishCultureInfo = "en-GB";

        public static class ConnectionStrings
        {
            public const string DefaultConnection = "DefaultConnection";
        }

        public static class PaginationData
        {
            public const int SongsPerPage = 12;
            public const int SongsViewHistoryPerPage = 12;
            public const int PlaylistsPerPage = 12;
            public const int PlaylistsSongsPerPage = 12;
            public const int LikedSongsPerPage = 12;
            public const int LicensesPerPage = 12;
            public const int CommentsPerPage = 10;
        }

        public static class DateTimeFormats
        {
            public const string Default = "MM/dd/yyyy";
            public const string Dash = "MM-dd-yyyy";
            public const string Time = @"hh\:mm";
            public const string MachineFormat = "yyyy-MM-dd'T'HH:mm:ss";
        }

        public static class VerificationCodeSpecification
        {
            public const int EndTimeInHours = 3;
            public const string RandomCodeCharacters = "abcdefghijklmnopqrstuvwxyABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            public const int RandomCodeLength = 5;
        }

        public static class Roles
        {
            public const string AdministratorRoleName = "Administrator";
            public const string UserRoleName = "User";
        }

        public static class Messeges
        {
            public static string ChangePassword(string userName, string code)
                => $"Hello {userName}, We got a request to reset your " + GlobalConstants.SystemName + " password."
                    + $"Verification code is {code}.";
        }

        public const int UsernameMinLength = 5;
        public const int UsernameMaxLength = 30;

        public const int FirstNameMinLength = 1;
        public const int FirstNameMaxLength = 30;

        public const int LastNameMinLength = 1;
        public const int LastNameMaxLength = 30;

        public const int PasswordMinLength = 8;
        public const int PasswordMaxLength = 20;

        public static class Email
        {
            public const string SystemEmail = "info@dreamAIMusic.com";
            public const string VerificationCodeCharacters = "abcdefghijklmnopqrstuvwxyABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            public const string ApiKey = "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";
            public const int VerificationCodeLength = 6;
        }

        public static class Folder
        {
            public const string SongFolderPath = "/Songs/";
            public const string MusicPosterPath = "/images/MusicHub.jpg";
        }

        public static class CreateFile
        {
            public const string RandomNameCharacters = "abcdefghijklmnopqrstuvwxyABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            public const int RandomNameLength = 5;
        }
    }
}
