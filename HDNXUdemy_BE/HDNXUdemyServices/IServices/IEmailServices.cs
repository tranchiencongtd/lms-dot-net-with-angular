namespace HDNXUdemyServices.IServices
{
    public interface IEmailServices
    {
        Task<bool> SendEmailToSingUpEmail(string email, string link);
    }
}