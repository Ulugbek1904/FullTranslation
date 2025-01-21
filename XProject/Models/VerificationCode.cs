using System;

namespace XProject.Models
{
    public class VerificationCode
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
