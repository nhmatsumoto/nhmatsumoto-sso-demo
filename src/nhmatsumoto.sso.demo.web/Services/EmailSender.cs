using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace nhmatsumoto.sso.demo.web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        private async Task Execute(string email, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = _emailSettings.Host,
                    Port = _emailSettings.Port,
                    EnableSsl = _emailSettings.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password)
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                message.To.Add(email);

                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // Trate as exceções adequadamente
                Console.WriteLine(ex.Message);
            }
        }
    }
}
