namespace HDNXUdemyServices.IServices
{
    public interface ISendEmailSMSServices
    {
        Task<bool> SendEmailToSingleEmail(string toEmail, string subjectEmail, string bodyEmail);

        Task<bool> SendEmailToListEmail(List<string> toEmail, string subjectEmail, string bodyEmail);
    }
}