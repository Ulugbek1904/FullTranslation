using System;

namespace XProject.Exceptions.UserException
{
    public class UserNullException : Exception
    {
        public UserNullException(string message) : base(message) { }
    }
}
