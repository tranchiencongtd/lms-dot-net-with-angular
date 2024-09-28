namespace HDNXUdemyServices.IServices
{
    public interface IClientAPIServices : IConfigClientAPIServices
    {
        Task<bool> GeneralTokenUploadServer(string email);
    }
}