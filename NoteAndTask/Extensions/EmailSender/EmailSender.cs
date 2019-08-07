using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace NoteAndTask.Extensions.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private EmailSettings EmailSettings { get; }

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            EmailSettings = emailSettings.Value;
        }

        public async Task Sender(string mailTo, string mailSubject, string mailBody)
        {
            MailMessage m = new MailMessage(
                new MailAddress(EmailSettings.FromEmail, EmailSettings.DisplayName),
                new MailAddress(mailTo))
            {
                Subject = mailSubject,
                Body = mailBody
            };

            await new SmtpClient(EmailSettings.PrimaryDomain, EmailSettings.PrimaryPort)
            {
                Credentials = new NetworkCredential(EmailSettings.UsernameEmail, EmailSettings.UsernamePassword),
                EnableSsl = true
            }.SendMailAsync(m);
        }


        public void SendEmail(string userEmail, string userTheme, string userMessage)
        {
            Sender(userEmail, userTheme, userMessage).GetAwaiter();
        }

        public async Task SendEmailAsync(string userEmail, string userTheme, string userMessage)
        {
            await Sender(userEmail, userTheme, userMessage);
        }
    }
}