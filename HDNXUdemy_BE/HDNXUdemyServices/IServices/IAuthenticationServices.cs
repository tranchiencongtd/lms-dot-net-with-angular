using HDNXUdemyModel.RequestModel;
using HDNXUdemyModel.ResponModel;
using Microsoft.AspNetCore.Http;

namespace HDNXUdemyServices.IServices
{
    public interface IAuthenticationServices
    {
        Task<ResponeLogin> LoginWithGoogle(LoginWithGoogle model, HttpContext httpContext);

        Task<ResponeLogin> LoginWithFacebook(LoginWithGoogle credential, HttpContext httpContext);

        Task<bool> RegisterNormalUser(string email, string password, string name, string phone);

        Task<ResponeLogin> LoginNormalAccount(string email, string password, HttpContext httpContext);

        Task<ResponeLogin> GeneralToken(string email, HttpContext httpContext);

        Task<bool> IsActiveAccountAfterRegister(string email, long id);
    }
}