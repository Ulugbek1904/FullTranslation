namespace XProject.Services.ValidationServices
{
    public interface IValidationService
    {
        public void ValidatePassword(string password);
        public void ValidateEmail(string email);
    }
}