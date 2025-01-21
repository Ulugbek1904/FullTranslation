using System;

namespace XProject.Exceptions.UserException
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() :
            base("Invalid email format. Please provide a valid email address") { }
    }
}
