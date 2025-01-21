using System;

namespace XProject.Exceptions.UserException
{
    public class InvalidPasswordFormatException : Exception
    {
        public InvalidPasswordFormatException() : 
            base("Password must be at least 6 characters long and contain both letters and numbers") { }
        
    }
}
