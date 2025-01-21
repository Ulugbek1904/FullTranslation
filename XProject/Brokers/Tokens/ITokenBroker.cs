using System.Threading.Tasks;
using XProject.Models;

namespace XProject.Brokers.Tokens
{
    public interface ITokenBroker
    {
        public string GenerateAccessToken(User user);
        public bool ValidateToken(string token);
        public string GetEmailFromToken(string token);

    }
}
