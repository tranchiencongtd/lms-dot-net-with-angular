using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.IServices;
using System.Net;

namespace HDNXUdemyAPI.Middlewares
{
    /// <summary>
    /// ExceptionMiddleware
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogServices<ExceptionMiddleware> _loggerMiddlewareException;

        /// <summary>
        /// ExceptionMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerMiddlewareException"></param>
        public ExceptionMiddleware(RequestDelegate next, ILogServices<ExceptionMiddleware> loggerMiddlewareException)
        {
            _next = next;
            _loggerMiddlewareException = loggerMiddlewareException;
        }

        /// <summary>
        /// InvokeAsync
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                _loggerMiddlewareException.LogInformation(ETypeAction.Middleware, "Start Middleware");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _loggerMiddlewareException.LogError(ETypeAction.Middleware, ex.Message, ex);
                httpContext.Response.StatusCode = (int)ExceptionStatusCode.GetExceptionStatusCode(ex);
                RepositoryModel<dynamic> result = new()
                {
                    PartnerCode = InterCode.ErrorMiddleware,
                    RetCode = ERetCode.SystemError,
                    Data = false,
                    StatusCode = (int)httpContext.Response.StatusCode,
                };

                switch (httpContext.Response.StatusCode)
                {
                    case (int)HttpStatusCode.BadRequest:
                        result.StatusCode = (int)HttpStatusCode.BadRequest;
                        result.SystemMessage = ex.Message;
                        result.Data = false;
                        break;

                    case (int)HttpStatusCode.Unauthorized:
                        result.StatusCode = (int)HttpStatusCode.Unauthorized;
                        result.SystemMessage = ex.Message;
                        result.Data = false;
                        break;

                    case (int)HttpStatusCode.NotFound:
                        result.StatusCode = (int)HttpStatusCode.NotFound;
                        result.SystemMessage = ex.Message;
                        result.Data = false;
                        break;

                    case (int)HttpStatusCode.UnavailableForLegalReasons:
                        result.StatusCode = (int)HttpStatusCode.UnavailableForLegalReasons;
                        result.SystemMessage = ex.Message;
                        result.Data = false;
                        break;

                    default:
                        result.StatusCode = (int)HttpStatusCode.InternalServerError;
                        result.SystemMessage = ex.Message;
                        result.Data = false;
                        break;
                }

                await httpContext.Response.WriteAsJsonAsync<RepositoryModel<dynamic>>(result);
            }
        }
    }

    /// <summary>
    /// MiddlewareExtensions
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// UseExceptionMiddleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}