using System;

namespace XProject.Exceptions.UserException
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { } 
    }
}
