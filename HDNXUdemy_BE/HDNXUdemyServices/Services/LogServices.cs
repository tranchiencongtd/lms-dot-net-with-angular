using HDNXUdemyModel.Constant;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.CommonFunction;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HDNXUdemyServices.Services
{
    public class LogServices<T> : ILogServices<T>
    {
        private readonly ILogger<T> _log;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogServices(ILogger<T> log, IHttpContextAccessor httpContextAccessor)
        {
            _log = log ?? throw new ProjectException(nameof(_log));
            _httpContextAccessor = httpContextAccessor ?? throw new ProjectException(nameof(_httpContextAccessor));
        }

        public void LogInformation(ETypeAction typeAction, string description)
        {
            int idCurrentUser = _httpContextAccessor.GetCurrentUserId();
            _log.LogInformation("{TypeAction} {Description} {UserId} {UserId}", (int)typeAction, description, idCurrentUser, DateTime.UtcNow);
        }

        public void LogWarring(ETypeAction typeAction, string description)
        {
            int idCurrentUser = _httpContextAccessor.GetCurrentUserId();
            _log.LogWarning("{TypeAction} {Description} {UserId} {UserId}", (int)typeAction, description, idCurrentUser, DateTime.UtcNow);
        }

        public void LogError(ETypeAction typeAction, string description, Exception exception)
        {
            int idCurrentUser = _httpContextAccessor.GetCurrentUserId();
            _log.LogError(exception, "{TypeAction} {Description} {UserId} {UserId}", (int)typeAction, description, idCurrentUser, DateTime.UtcNow);
        }

        public void LogError(ETypeAction typeAction, string description)
        {
            int idCurrentUser = _httpContextAccessor.GetCurrentUserId();
            _log.LogError("{TypeAction} {Description} {UserId} {UserId}", (int)typeAction, description, idCurrentUser, DateTime.UtcNow);
        }

        public void LogTrace(ETypeAction typeAction, string description)
        {
            int idCurrentUser = _httpContextAccessor.GetCurrentUserId();
            _log.LogTrace("{TypeAction} {Description} {UserId} {UserId}", (int)typeAction, description, idCurrentUser, DateTime.UtcNow);
        }
    }
}