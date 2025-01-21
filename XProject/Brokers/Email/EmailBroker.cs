using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace XProject.Brokers.Email
{
    public class EmailBroker : IEmailBroker
    {
        private readonly IConfiguration configuration;

        public EmailBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetEmailFromToken(string token)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendAccessTokenByEmailAsync(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(this.configuration["Email:EmailAddress"]))
                throw new ArgumentException("Email address is not configured.");

            if (string.IsNullOrWhiteSpace(this.configuration["Email:Password"]))
                throw new ArgumentException("Email password is not configured.");

            using var smtpClient = new SmtpClient(this.configuration["Email:Host"])
            {
                Port = 587,
                Credentials = new NetworkCredential(
                    this.configuration["Email:EmailAddress"],
                    this.configuration["Email:Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage()
            {
                From = new MailAddress(this.configuration["Email:EmailAddress"]),
                Subject = "Registration Confirmation",
                Body = "",
                IsBodyHtml = false
            };

            mailMessage.To.Add(email);
            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient
            {
                Port = 587,
                Host = this.configuration["Email:Host"],
                Credentials = new NetworkCredential(
                    this.configuration["Email:EmailAddress"],
                    this.configuration["Email:Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(this.configuration["Email:EmailAddress"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);

        }
    }
}
