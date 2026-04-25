namespace MusicHub.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;
    using MusicHub.Web.ViewModels.MailModels;

    public class EmailSender : IEmailSender
    {
        private readonly string smtpHost = "smtp.abv.bg";
        private readonly int smtpPort = 465;
        private readonly string username = "musichub@abv.bg";
        private readonly string password = "MusicHub2026";
        private readonly string name = "Music Hub";
        private readonly string fromEmail = "musichub@abv.bg";

        public async Task SendEmailAsync(string toMail, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(this.name, this.fromEmail));
            message.To.Add(MailboxAddress.Parse(toMail));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = messageBody,
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(this.smtpHost, this.smtpPort, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(this.username, this.password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendEmailAsync(EmailInputModel model)
        {
            await this.SendEmailAsync(model.ToMail, model.Subject, model.MessageBody);
        }
    }
}
