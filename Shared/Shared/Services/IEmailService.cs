namespace Shared.Services
{
    public interface IEmailService
    {
        Task SendEmail(string toEmail, string subject, string body, bool isBodyHTML);
    }
}
