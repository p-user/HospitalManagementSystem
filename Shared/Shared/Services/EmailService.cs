
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net.Mail;
using System.Net;

namespace Shared.Services
{
    public class EmailService  : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration _configuration)
        {
            configuration=_configuration;
        }

        public async Task SendEmail(string toEmail, string subject, string body, bool isBodyHTML)
        {
            string MailServer = configuration["EmailSettings:MailServer"];
            string FromEmail = configuration["EmailSettings:FromEmail"];
            string Password = configuration["EmailSettings:Password"];
            int Port = int.Parse(configuration["EmailSettings:MailPort"]);
            var client = new SmtpClient(MailServer, Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromEmail, Password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            MailMessage mailMessage = new MailMessage(FromEmail, toEmail, subject, body)
            {
                IsBodyHtml = isBodyHTML
            };

            await client.SendMailAsync(mailMessage);

        }
    }
}
