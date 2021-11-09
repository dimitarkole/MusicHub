namespace MusicHub.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class ModelConstants
    {

        public static class CategotyConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;
        }

        public static class MusicConstants
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 60;
            public const int DescriptionMinLength = 1;
            public const int DescriptionMaxLength = 1000;
        }

        public static class AuthtneticationConstants
        {
            public const int TokenExpirationTimeInDays = 3;
        }

        public static class UserConstants
        {
            public const int NameMaxlength = 60;
            public const int PasswordMinLength = 6;
        }

        public static class NotificationConstants
        {
            public const int ContentMaxLength = 1000;
        }

        public static class VerificationCodeConstants
        {
            public const int EndTimeInHours = 2;
            public const string RandomCodeCharacters = "abcdefghijklmnopqrstuvwxyABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            public const int RandomCodeLength = 5;
        }
    }
}
