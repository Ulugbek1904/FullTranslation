using System.Text.RegularExpressions;
using XProject.Exceptions.UserException;

namespace XProject.Services.ValidationServices
{
    public class ValidationService : IValidationService
    {
        private readonly Regex emailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        private readonly Regex passwordRegex = new(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$");

        public void ValidateEmail(string email)
        {
            if (!emailRegex.IsMatch(email))
                throw new InvalidEmailException();
        }

        public void ValidatePassword(string password)
        {
            if (!passwordRegex.IsMatch(password))
                throw new InvalidPasswordFormatException();
        }
    }

}
