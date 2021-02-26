using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Common
{
    public static class ModelConstants
    {

        public static class CategotyConstants
        {
            public const int NameMinLength = 60;
            public const int NameMaxLength = 1;
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
    }
}
