using MusicHub.Common;
using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.VerificationCodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public static class VerificationCodeTestsData
    {
        public static List<User> Users = UserTestsData.Users;

        public static string UserId = UserTestsData.Users[0].Id;

        public static VerificationCodeCreateInputModel CreateModel => new VerificationCodeCreateInputModel()
        {
            Type = VerificationType.ChangePassword,
            UserId = UserId,
        };

        public static List<VerificationCode> VerificationCodes()
        {
            var result = new List<VerificationCode>();
            int id = 1;
            foreach (var user in Users)
            {
                var random = new Random();
                var length = 6;
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var code = new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
                var isUsed = random.Next(0, 2) >= 1;
                var verificationType = random.Next(0, 2) >= 0 ? VerificationType.ConfirmEmail: VerificationType.ChangePassword;
                var verificationCode = new VerificationCode()
                {
                    UserId = user.Id,
                    Code = code,
                    IsUsed = isUsed,
                    Id = "VerificationCodeId" + id,
                    Type = verificationType,
                };

                id++;
                result.Add(verificationCode);
            }

            return result;
        }
    }
}
