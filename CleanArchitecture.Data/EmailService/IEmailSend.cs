

namespace CleanArchitecture.Data.EmailService
{
    public interface IEmailSend
    {
        Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true);

    }
}
