namespace MusicHub.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SendGrid;
    using SendGrid.Helpers.Mail;
    using MusicHub.Services.Messaging.Infrastucture.Extensions;
    using static MusicHub.Common.GlobalConstants;
    using MusicHub.Web.ViewModels.MailModels;

    public class SendGridEmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string toMail, string subject, string messageBody)
        {
            var apiKey = Email.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Email.SystemEmail, SystemName);
            var to = new EmailAddress(toMail);
            messageBody = messageBody.MakeSringToMessageWithTemplate();
            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                messageBody,
                messageBody);
            await client.SendEmailAsync(msg);
        }

        public async Task SendEmailAsync(EmailInputModel model)
            => await this.SendEmailAsync(model.ToMail, model.Subject, model.MessageBody);
    }
}
