using HDNXUdemyModel.Base;

using System.Runtime.Serialization;

namespace HDNXUdemyModel.Exception
{
    using System;

    public class ProjectBadRequestException : Exception
    {
        public ProjectBadRequestException()
        {
        }

        public ProjectBadRequestException(string message) : base($"{ProjectConfig.AppName}: {message}")
        {
        }

        public ProjectBadRequestException(string messags, Exception inner) : base($"{ProjectConfig.AppName}: {messags}", inner)
        {
        }

        public ProjectBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}