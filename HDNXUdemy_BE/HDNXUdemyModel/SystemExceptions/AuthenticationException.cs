namespace HDNXUdemyModel.Exceptions
{
    using HDNXUdemyModel.Base;
    using System;
    using System.Runtime.Serialization;

    public class AuthenticationException : Exception
    {
        public AuthenticationException()
        {
        }

        public AuthenticationException(string message) : base($"{ProjectConfig.AppName}: {message}")
        {
        }

        public AuthenticationException(string message, Exception inner) : base($"{ProjectConfig.AppName}: {message}", inner)
        {
        }

        public AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}