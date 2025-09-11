using System.Net;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;


namespace CleanArchitecture.Data.EmailService
{
    public class EmailSend : IEmailSend
    {
        private readonly ILogger<EmailSend> _logger;
        private readonly EmailSettings _settings;

        public EmailSend(IOptions<EmailSettings> options, ILogger<EmailSend> logger)
        {
            _settings= options.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
        {
            try {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_settings.Username, _settings.SenderEmail));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;
                var bodyBuilder = new BodyBuilder();
                if (isHtml) {
                    bodyBuilder.HtmlBody = body;
                } else {
                    bodyBuilder.TextBody = body;
                }
                message.Body = bodyBuilder.ToMessageBody();
                
                var client = new SmtpClient();
               
                await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);
               
                await client.AuthenticateAsync(_settings.Username, _settings.Password);
               
                await client.SendAsync(message);
               
                await client.DisconnectAsync(true);
            }
            catch (Exception ex) { 
            
            
            }
        }
    }
}