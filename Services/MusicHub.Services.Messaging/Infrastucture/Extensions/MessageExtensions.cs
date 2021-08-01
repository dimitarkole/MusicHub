using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Services.Messaging.Infrastucture.Extensions
{
    public static class MessageExtensions
    {
        public static string MakeSringToMessageWithTemplate(this string content)
        {
            var result = new StringBuilder();
            result.AppendLine(MessageStart);
            result.AppendLine(MessageHeader);
            result.AppendLine(MessageBody(content));
            result.AppendLine(MessageFooter);
            result.AppendLine(MessageEnd);
            return result.ToString().Trim();
        }

        private static string MessageStart => "<table width='100%' style='text-align:center;borde" +
            "r-collapse:collapse;font-family:Verdana, Helvetica, sans-serif;width:100.0%;' border='0' cellpadding" +
            "='0' cellspacing='0' align='center'><tbody><tr><td>&nbsp;</td><td> <table width='700' style='margin:" +
            "auto;background-color:#fafafa;border-collapse:collapse;font-family:Arial, Helvetica, sans-serif;widt" +
            "h:700px;color:#666666;' border='0' cellpadding='0' cellspacing='0' bgcolor='#fafafa' align='center'>" +
            "<tbody><tr><td width='700' style='width:700px;'>";

        private static string MessageHeader => "<table width='700' style='" + MessagesStyle.BackgroundHeaderStyle +
            " ' bgcolor='#b32843' cellpadding='0' cellspacing='0'><tbody><tr><td width='49' heig" +
            "ht='35' style='width:49px;height:35px;'><a href='" + URLs.SiteUrl + "' target='_blank'> <td width='65" +
            "1px' height='35' style='width:651px;height:35px; line-height: 0.75; font-size: 30px; font-weight: 60" +
            "0; color: #ffffff; margin-left: 3px; transform: translateY(4px); font-family: 'Josefin Sans', sans-s" +
            "erif;'>&nbsp;MusicHub</td> </tr></tbody></table><table width='700' style='background-color:#fafafa;b" +
            "order-collapse:collapse;width:700px;' bgcolor='#f5f5f5' border='0' cellpadding='0' cellspacing='0'><" +
            "tbody><tr> <td width='700' height='50' style='width:700px;height:50px;line-height:40px;font-size:40p" +
            "x;'>&nbsp;</td> </tr> </tbody></table>";

        private static string MessageBody(string content) => "<table width='700' style='background-color:#fafafa" +
            ";border-collapse:collapse;width:700px;' bgcolor='#f5f5f5' border='0' cellpadding='0' cellspacing='0'" +
            "><tbody><tr><td style='width:19px;' width='19'>&nbsp;</td><td> <table width='660' style='width:660px" +
            ";border-collapse:collapse;' cellpadding='0' cellspacing='0' border='0'><tbody><tr><td style='width:6" +
            "60px;' width='660'> <div style='box-sizing:border-box;width:660px;border-radius:6px;border:1px solid" +
            " #d5d5d5;background-color:#ffffff;overflow:hidden;'><table width='658' style='min-width:100.0%;width" +
            ":658px;border-collapse:collapse;' cellpadding='0' cellspacing='0' border='0'> <tbody> <tr> <td colsp" +
            "an='3' style='height:20px;'>&nbsp;</td></tr><tr> <td width='618' bgcolor='#ffffff' style='width:618p" +
            "x;vertical-align:top;'> <table style='text-align:left;color:#333333;background-color:#ffffff;width:1" +
            "00.0%;border-collapse:collapse;' border='0' cellspacing='0' cellpadding='0'><tbody> " + content + " " +
            "</tbody></table></td></tr><tr> <td colspan='3' style='height:20px;'>&nbsp;</td></tr> </tbody> </tabl" +
            "e> </div> </td> </tr>";

        private static string MessageFooter => "<tr><td style='width:662px;' width='662'> <table s" +
            "tyle='text-align:left;color:#333333;background-color:#fafafa;width:662px;border-collapse:collapse;' " +
            "border='0' width='662' cellspacing='0' cellpadding='0'><tbody><tr><td colspan='3' height='30' style=" +
            "'line-height:14px;height:30px;'>&nbsp;</td> </tr> <tr><td style='width:40px;' width='40'>&nbsp;</td>" +
            "<td><table style='margin:0;text-align:center;max-width:100.0%;border-collapse:collapse;padding:0;' b" +
            "order='0' cellspacing='0' cellpadding='0'><tbody> <tr><td style='color:#666666;font-size:14px;line-h" +
            "eight:19px;font-weight:normal;text-align:left;'>Kind regards,<br>MusicHub Team</td> </tr></tbody> </" +
            "table></td><td style='width:40px;' width='40'>&nbsp;</td> </tr> <tr><td colspan='3' height='20' styl" +
            "e='line-height:14px;height:20px;'>&nbsp;</td> </tr> <tr><td style='width:40px;' width='40'>&nbsp;</t" +
            "d><td style='max-width:100.0%;color:#4b4b4b;font-size:10px;line-height:16px;font-weight:normal;paddi" +
            "ng:0;margin:0;'> This is an automatically generated email, do not reply to this message.<br> If you " +
            "have any issues, please <a style='color:#7192c7;text-decoration:underline;' href='" + URLs.SiteUrl +
            "'target='_blank'>contact</a> MusicHub support.<br> Please read our <a style='color:#7192c7;text-de" +
            "coration:underline;' href='" + URLs.FAQ + "' target='_blank'>FAQ</a> to learn more abo" +
            "ut MusicHub.<br> To stop receiving our email notifications, please<a style='color:#7192c7;text-decor" +
            "ation:underline;' href='" + URLs.NotificationSettings + "'target='_blank'>click" +
            " here</a>. </td><td style='width:40px;' width='40'>&nbsp;</td> </tr> <tr><td colspan='3' height='20'" +
            " style='line-height:14px;height:20px;'>&nbsp;</td> </tr></tbody></table> </td> </tbody></table> </td" +
            "><td style='width:19px;' width='19'>&nbsp;</td> </tr></tbody></table><table bgcolor='#a02239' width=" +
            "'700px' style='height:35px;width:700px;text-align:left;background-color: #3b125d;background-image: l" +
            "inear-gradient(to right, #2457d7, #3b125d);background: #3b125d;background: linear-gradient(to right," +
            " #2457d7, #3b125d);z-index: 100;' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td width='7" +
            "00px' style='width:700px;font-size:1px;height:35px;text-align:center;vertical-align:middle;'> <a href=" +
            "'https://blog.MusicHub.com/en' style='" + MessagesStyle.FooterTextStyle + "' target='_blank'>Music" +
            "Hub Blog</a> <span style='color:#ffffff;font-size:12px;'>&nbsp;&nbsp;</span> <a href='" + URLs.Twitter +
            "' style='" + MessagesStyle.FooterTextStyle + "' target='_blank'>MusicHub on Twitter</" +
            "a> <span style='color:#ffffff;font-size:12px;'>&nbsp;&nbsp;</span><a href='" + URLs.SiteUrl +
            "' style='" + MessagesStyle.FooterTextStyle + "' target='_blank'>Privacy Policy</a> </td> </tr><" +
            "/tbody></table>";

        private static string MessageEnd => "</td> </tr> </tbody></table> </td><td>&nbsp;</td> " +
            "</tr></tbody></table>";
    }
}
