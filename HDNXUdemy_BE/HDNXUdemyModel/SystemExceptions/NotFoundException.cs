using HDNXUdemyModel.Base;
using System.Runtime.Serialization;

namespace HDNXUdemyModel.SystemExceptions
{
    using System;

    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException()
        {
        }

        public ProjectNotFoundException(string message) : base($"{ProjectConfig.AppName}: {message}")
        {
        }

        public ProjectNotFoundException(string message, Exception inner) : base($"{ProjectConfig.AppName}: {message}", inner)
        {
        }

        public ProjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}