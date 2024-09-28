using System.Net;

namespace HDNXUdemyModel.SystemExceptions
{
    using HDNXUdemyModel.Exception;
    using System;
    using System.Security.Authentication;

    public static class ExceptionStatusCode
    {
        private static Dictionary<Type, HttpStatusCode> exceptionStatusCode = new()
        {
            { typeof(ProjectBadRequestException), HttpStatusCode.BadRequest },
            { typeof(ProjectNotFoundException), HttpStatusCode.NotFound },
            { typeof(AuthenticationException), HttpStatusCode.Unauthorized },
            { typeof(ProjectAwsException), HttpStatusCode.InternalServerError },
            { typeof(ProjectException), HttpStatusCode.InternalServerError },
        };

        public static HttpStatusCode GetExceptionStatusCode(Exception exception)
        {
            bool exceptionFound = exceptionStatusCode.TryGetValue(exception.GetType(), out var statusCode);
            return exceptionFound ? statusCode : HttpStatusCode.InternalServerError;
        }
    }
}