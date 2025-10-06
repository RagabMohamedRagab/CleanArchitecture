using System.Net;
using System.Net.Mail;
using CleanArchitecture.Data.IEmailService;
using CleanArchitecture.Service.Helpers;
using MimeKit;

namespace CleanArchitecture.Infrastructure.EmailService
{
    public class EmailSender(EmailSettings emailSettings) : IEmailSender
    {
        private readonly EmailSettings _emailSettings = emailSettings;

        public async Task SendEmailAsync(string email, string subject, string content)
        {
            var mailMSG = new MailMessage();
            mailMSG.To.Add(email);
            mailMSG.From = new MailAddress(_emailSettings.SenderEmail);
            mailMSG.Subject = subject;
            mailMSG.IsBodyHtml = true;
            mailMSG.Body = $@"
    <html>
    <body style='font-family: Arial, sans-serif; margin: 20px;'>
        <h2 style='color: #333;'>{subject}</h2>
        <div style='background-color: #f9f9f9; padding: 15px; border-left: 4px solid #007bff;'>
            {content}
        </div>
        <br>
        <p style='color: #666; font-size: 12px;'>
            Best regards,<br>
            Your Application Team
        </p>
    </body>
    </html>";


            using var client = new SmtpClient {
                Host = _emailSettings.SmtpServer,
                Port = _emailSettings.SmtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password)
            };

            await client.SendMailAsync(mailMSG);
        }
    }
}
