namespace MusicHub.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;

    using static MusicHub.Common.GlobalConstants;

    public static class MessagesContentTemplates
    {
        public static string CreateMessageContent(MessageType messageType, Dictionary<string, string> data)
        {
            switch (messageType)
            {
                case MessageType.ConfirmEmail: return ConfirmEmailTemplate(data);
                default: return "";
            }
        }

        private static string ConfirmEmailTemplate(Dictionary<string, string> data)
        {
            return "<tr> <td style='height:30px;font-size:1px;line-hei" +
            "ght:1px;' colspan='3' height='30'>&nbsp;</td></tr><tr> <td style='width:40px;' width='40'>&nbsp;</td" +
            "> <td style='color:#2c1485;font-size:34px;line-height:38px;font-family:Arial, Helvectica, sans-serif" +
            ";text-align:center;'>Hello, "+ data["userName"] +"!</td> <td style='width:40px;' width='40'>&nbsp;</td></tr><tr> <td s" +
            "tyle='height:30px;font-size:1px;line-height:1px;' colspan='3' height='30'>&nbsp;</td></tr><tr> <td s" +
            "tyle='width:40px;' width='40'>&nbsp;</td> <td style='font-size:17px;line-height:19px;color:#666666;f" +
            "ont-weight:normal;text-align:center;'>To complete your registration on " + SystemName + ",</td> <td style='wid" +
            "th:40px;' width='40'>&nbsp;</td></tr><tr> <td style='width:40px;' width='40'>&nbsp;</td> <td style='" +
            "color:#548ad2;font-size:17px;font-weight:bold;text-align:center;line-height:19px;'>please confirm yo" +
            "ur email by clicking the button below.</td> <td style='width:40px;' width='40'>&nbsp;</td></tr><tr> " +
            "<td style='height:35px;font-size:1px;line-height:1px;' colspan='3' height='35'>&nbsp;</td></tr><tr> " +
            "<td style='height:90px;background-color:#fafafa;vertical-align:middle;text-align:center;' colspan='3" +
            "' height='90'><div><a style='background-color:#4f84c9;border:1px solid #4f84c9;border-radius:4px;col" +
            "or:#ffffff;display:inline-block;font-family:sans-serif;font-size:15px;font-weight:bold;line-height:5" +
            "0px;text-align:center;text-decoration:none;width:261px;' href='" + URLs.ConfirmEmail +
            "/" + data["userId"] + "' target='_blank'>Confirm Your Email</a> </div> </td></tr><tr> <td " +
            "style='height:35px;font-size:1px;line-height:1px;' colspan='3' height='35'>&nbsp;</td></tr><tr> <td " +
            "style='width:40px;' width='40'>&nbsp;</td> <td style='text-align:left;color:#666666;font-size:14px;f" +
            "ont-family:sans-serif;line-height:19px;'>You can also copy and <span style='font-weight:bold;'>paste" +
            " this link into your browser:</span></td> <td style='width:40px;' width='40'>&nbsp;</td></tr><tr> <t" +
            "d style='width:40px;' width='40'>&nbsp;</td> <td style='text-align:left;color:#558cd1;font-size:14px" +
            ";font-family:sans-serif;line-height:19px;'><a style='color:#558cd1;line-height:19px;text-decoration:" +
            "underline;' href='" + URLs.ConfirmEmail +
            "/" + data["userId"] + "' target='_b" +
            "lank'>'" + URLs.ConfirmEmail +
            "/" + data["userId"] + "'</a></td> <td style='wid" +
            "th:40px;' width='40'>&nbsp;</td></tr><tr> <td style='height:30px;font-size:1px;line-height:1px;' col" +
            "span='3' height='30'>&nbsp;</td></tr><tr> <td style='width:40px;' width='40'>&nbsp;</td> <td style='" +
            "text-align:left;font-size:14px;line-height:19px;color:#666666;font-family:sans-serif;'><span style='" +
            "font-weight:bold;color:#2c1485;'>NB!</span> The account will be reviewed by the site administration " +
            "only after the email address is confirmed.</td> <td style='width:40px;' width='40'>&nbsp;</td></tr> ";
        }
    }
}
