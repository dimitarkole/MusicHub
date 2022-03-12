using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHub.Web.Tests.TestData.Settings
{
    public class FakeAppSettings
    {
        public const string SiteUrl = "https://casemanager";

        public const string Key = "authenticationtokenvalidationkey";

        public const string FileTokenKey = "filetokenvalidationkey";

        public static ApplicationSettings GetAppSettings() =>
            new ApplicationSettings
            {
                Secret = Key,
            };
    }
}
