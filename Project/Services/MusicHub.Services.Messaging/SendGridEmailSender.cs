namespace MusicHub.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Web.ViewModels.MailModels;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    using static MusicHub.Common.GlobalConstants;

    public class SendGridEmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string toMail, string subject, string messageBody)
        {
            var apiKey = Email.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Email.SystemEmail, SystemName);
            var to = new EmailAddress(toMail);

            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                messageBody,
                messageBody);

            await client.SendEmailAsync(msg);
        }

        public async Task SendEmailAfterUserRegistration(string to, string username, string password)
        {
            var subject = "Successful registration";
            var htmlContent = "You have regestered sucsessful in" + SystemName + "\n" + "username: " + username + "\n" + "password: " + password;
            await this.SendEmailAsync(to, subject, htmlContent);
        }

        public async Task SendEmailAsync(EmailInputModel model)
        {
            var apiKey = Email.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Email.SystemEmail, SystemName);
            var to = new EmailAddress(model.ToMail);

            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                model.Subject,
                model.MessageBody,
                model.MessageBody);

            await client.SendEmailAsync(msg);
        }
    }
}
