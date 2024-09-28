namespace HDNXUdemyModel.SystemExceptions
{
    using HDNXUdemyModel.Base;
    using System;

    public class ProjectException : Exception
    {
        public ProjectException()
        {
        }

        public ProjectException(string message) : base($"{ProjectConfig.AppName}: {message}")
        {
        }

        public ProjectException(string message, Exception innerException) : base($"{ProjectConfig.AppName}: {message}", innerException)
        {
        }
    }
}