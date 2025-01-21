using System.Threading.Tasks;

namespace XProject.Brokers.Email
{
    public interface IEmailBroker
    {
        public Task SendAccessTokenByEmailAsync(string email, string token);
        public string GetEmailFromToken(string token);
        Task SendEmailAsync(string to, string subject, string body);
    }
}