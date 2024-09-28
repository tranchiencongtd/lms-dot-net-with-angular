using HDNXUdemyModel.Constant;

namespace HDNXUdemyServices.IServices
{
    public interface ILogServices<T>
    {
        void LogInformation(ETypeAction typeAction, string description);

        void LogWarring(ETypeAction typeAction, string description);

        void LogError(ETypeAction typeAction, string description, Exception exception);

        void LogError(ETypeAction typeAction, string description);

        void LogTrace(ETypeAction typeAction, string description);
    }
}