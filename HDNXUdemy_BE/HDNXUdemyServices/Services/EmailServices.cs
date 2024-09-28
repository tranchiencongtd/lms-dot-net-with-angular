using HDNXUdemyModel.Base;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.CommonFunction;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Hosting;

namespace HDNXUdemyServices.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ISendEmailSMSServices _sendEmailSMSServices;

        public EmailServices(ISendEmailSMSServices sendEmailSMSServices, IWebHostEnvironment hostingEnvironment)
        {
            _sendEmailSMSServices = sendEmailSMSServices ?? throw new ProjectException(nameof(_sendEmailSMSServices));
            _hostingEnvironment = hostingEnvironment ?? throw new ProjectException(nameof(_hostingEnvironment));
        }

        public async Task<bool> SendEmailToSingUpEmail(string email, string link)
        {
            try
            {
                string subjectSendEmailToSingUpEmail = "Email thông báo đăng ký thành công";
                string filePathSendToSender = $"{_hostingEnvironment.WebRootPath}/{ProjectConfig.EmailFolder}/{ProjectConfig.SendEmailSignupTemplate}";
                StreamReader streamReader = new(filePathSendToSender);
                string bodyEmailTemplate = await streamReader.ReadToEndAsync();
                streamReader.Close();
                bodyEmailTemplate = bodyEmailTemplate.FormatEmail(
                    urlVerify => link,
                    url => link);

                await _sendEmailSMSServices.SendEmailToSingleEmail(email, subjectSendEmailToSingUpEmail, bodyEmailTemplate);
                return true;
            }
            catch (Exception ex)
            {
                throw new ProjectException(ex.InnerException?.Message ?? string.Empty, ex);
            }
        }
    }
}