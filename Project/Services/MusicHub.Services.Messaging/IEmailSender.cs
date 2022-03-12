namespace MusicHub.Services.Messaging
{
    using MusicHub.Web.ViewModels.MailModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task SendEmailAsync(
            string toMail,
            string subject,
            string messageBody);

        Task SendEmailAsync(EmailInputModel model);

        Task SendEmailAfterUserRegistration(
            string to,
            string username,
            string password);
    }
}
