using System.Threading.Tasks;

namespace NoteAndTask.Extensions.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(string userEmail, string userTheme, string userMessage);
        Task SendEmailAsync(string userEmail, string userTheme, string userMessage);
    }
}