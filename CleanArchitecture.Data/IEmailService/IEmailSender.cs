namespace CleanArchitecture.Data.IEmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string content);

    }
}
