using HDNXUdemyServices.CommonFunction;

namespace HDNXUdemyServices.IServices
{
    public interface IConfigClientAPIServices
    {
        Task<BaseClientAPIServices> GetClientAPI();
    }
}