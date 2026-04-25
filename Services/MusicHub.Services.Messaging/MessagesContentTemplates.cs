namespace MusicHub.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;

    using static MusicHub.Common.GlobalConstants;

    public static class MessagesContentTemplates
    {
        public static string CreateMessageContent(string emailSubject, Dictionary<string, string> data)
        {
            switch (emailSubject)
            {
                case EmailSubjects.ConfirmEmail: return ConfirmEmailTemplate(data);
                case EmailSubjects.ChangePassword: return ChangePassword(data);
                case EmailSubjects.SuccessfulChangedPassword: return SuccessfulChangePassword(data);
                default: return null;
            }
        }

        private static string ConfirmEmailTemplate(Dictionary<string, string> data)
        {
            return $@"
        <div style='font-family: Arial, sans-serif; text-align: center;'>
            <h2>Confirm your email</h2>
            
            <p>Hello, {data["userName"]} </p>
            
            <p>To complete your registration in {SystemName}, please use the verification code below:</p>
            
            <div style='
                margin: 20px auto;
                padding: 15px;
                font-size: 24px;
                font-weight: bold;
                letter-spacing: 5px;
                background-color: #f4f4f4;
                display: inline-block;
                border-radius: 6px;'>
                {data["verificationCode"]}
            </div>

            <p>This code is valid for a limited time.</p>
            
            <p>If you didn’t request this, just ignore this email.</p>
        </div>";
        }

        private static string ChangePassword(Dictionary<string, string> data)
             => "<tr> " + Salutations(data["userName"]) + " <td style='width:40px;' width='40'>&nbsp;</td></tr><tr> <td s" +
            "tyle='height:30px;font-size:1px;line-height:1px;' colspan='3' height='30'>&nbsp;</td></tr><tr> <td s" +
            "tyle='width:40px;' width='40'>&nbsp;</td> <td style='font-size:17px;line-height:19px;color:#666666;f" +
            "ont-weight:normal;text-align:center;'>We got a request to reset your " + GlobalConstants.SystemName + " password."
                 + $"Verification code is " + data["verificationCode"] + ".</td></tr>";

        private static string SuccessfulChangePassword(Dictionary<string, string> data)
        => "<tr> " + Salutations(data["userName"]) + " <td style='width:40px;' width='40'>&nbsp;</td></tr><tr> <td s" +
           "tyle='height:30px;font-size:1px;line-height:1px;' colspan='3' height='30'>&nbsp;</td></tr><tr> <td s" +
           "tyle='width:40px;' width='40'>&nbsp;</td> <td style='font-size:17px;line-height:19px;color:#666666;f" +
           "ont-weight:normal;text-align:center;'>Your password has been changed successful.</td></tr>";

        private static string Salutations(string username)
            => "<td style='height:30px;font-size:1px;line-hei" +
            "ght:1px;' colspan='3' height='30'>&nbsp;</td></tr><tr> <td style='width:40px;' width='40'>&nbsp;</td" +
            "> <td style='color:#2c1485;font-size:34px;line-height:38px;font-family:Arial, Helvectica, sans-serif" +
            ";text-align:center;'>Hello, " + username + "!</td>";

        private static string Footer(string username)
            => "<td style='height:30px;font-size:1px;line-hei" +
            "ght:1px;' colspan='3' height='30'>&nbsp;</td></tr><tr> <td style='width:40px;' width='40'>&nbsp;</td" +
            "> <td style='color:#2c1485;font-size:34px;line-height:38px;font-family:Arial, Helvectica, sans-serif" +
            ";text-align:center;'>Hello, " + username + "!</td>";

        private static string ConfirmURL(string userId, string verificationCode)
            => URLs.ConfirmEmail + "/" + userId + "?code=" + verificationCode;
    }
}
