using MusicHub.Common;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Web.ViewModels.LicenseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public class LicenseTestsData
    {
        public static LicenseCreateInputModel CreateModel => new LicenseCreateInputModel()
        {
            Name = "TestLicenseName",
            Status = LicenseStatus.WaitToBeView,
        };

        public static LicenseCreateInputModel CreateEmptyModel => new LicenseCreateInputModel()
        {
            Name = "",
            Status = LicenseStatus.WaitToBeView,
        };

        public static LicenseFileCreateInputModel CreateFileModel => new LicenseFileCreateInputModel()
        {
            FileUrl = "FileUrl",
        };

        public static LicenseFileCreateInputModel CreateFileEmptyModel => new LicenseFileCreateInputModel()
        {
            FileUrl = "",
        };

        public static string CreateUserId => LicenseTestsData.Users[0].Id;

        public static LicenseStatusInputModel ChangeStatusModel => new LicenseStatusInputModel()
        {
            Status = LicenseStatus.Approve,
        };

        public static LicenseEditInputModel UpdateModel => new LicenseEditInputModel()
        {
            Name = "UpdateName",
        };

        public static LicenseEditInputModel UpdateEmptyModel => new LicenseEditInputModel()
        {
            Name = "",
        };

        public static string LicenseFilterUserId => LicenseTestsData.Users[0].Id;

        public static LicenseFilter LicenseFilter => new LicenseFilter()
        {
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static LicenseFilter LicenseFilterWithUserName => new LicenseFilter()
        {
            Name = "1",
            Status = LicenseStatus.Approve,
            OrderMethod = OrderMethod.CreatedOnAsc,
            Username = "1",
        };

        public static LicenseFilter LicenseFilterWithStatus => new LicenseFilter()
        {
            Name = "1",
            Status = LicenseStatus.Reject,
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static LicenseFilter LicenseFilterWithUserNameAndStatus => new LicenseFilter()
        {
            Name = "1",
            Status = LicenseStatus.WaitToBeView,
            OrderMethod = OrderMethod.CreatedOnAsc,
            Username = "1",
        };

        public static LicenseFilter LicenseFilterWithUserId => new LicenseFilter()
        {
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
            UserId = Users[0].Id,
        };

        public static LicenseFilter LicenseFilterWithUserIdAndStatus => new LicenseFilter()
        {
            Name = "1",
            Status = LicenseStatus.Approve,
            OrderMethod = OrderMethod.CreatedOnAsc,
            Username = "1",
        };

        public static List<User> Users => UserTestsData.Users;

        public static List<License> Licenses()
        {
            var result = new List<License>(0);
            var random = new Random();
            var users = Users;
            var count = 1;
            const int maxCountLicenseAddedByUser = 10;
            foreach (var user in users)
            {
                var countLicenses = random.Next(1, maxCountLicenseAddedByUser - 1);
                if (countLicenses > maxCountLicenseAddedByUser * 0.4)
                {
                    for (int i = 0; i < countLicenses; i++)
                    {
                        Type type = typeof(LicenseStatus);
                        Array values = type.GetEnumValues();
                        var licenseStatus = (LicenseStatus)values.GetValue(random.Next(values.Length));

                        var license = new License()
                        {
                            Id = "TestLicenseId" + count,
                            //LicenseFiles = new List<LicenseFile>(),
                            //MusicLicenses = new List<MusicLicense>(),
                            Name = "TestLicenseName" + count,
                            UserId = user.Id,
                            Status = licenseStatus,
                        };

                        result.Add(license);
                        count++;
                    }
                }
            }

            return result;
        }

        public static List<LicenseFile> LicenseFiles(List<License> licenses)
        {
            var result = new List<LicenseFile>();
            var random = new Random(0);
            var count = 1;
            const int maxCountFilesInLicense = 10;
            foreach (var license in licenses)
            {
                var countLicenseFiles = random.Next(1, maxCountFilesInLicense - 1);
                for (int i = 0; i < countLicenseFiles; i++)
                {
                    Type type = typeof(LicenseStatus);
                    Array values = type.GetEnumValues();
                    var licenseStatus = (LicenseStatus)values.GetValue(random.Next(values.Length));

                    var licenseFile = new LicenseFile()
                    {
                        Id = "TestLicenseId" + count,
                        FileUrl = "Test File url" + count,
                        LicensеId = license.Id,
                    };

                    result.Add(licenseFile);
                    count++;
                }
            }

            return result;
        }
    }
}
