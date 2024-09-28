using Asp.Versioning;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.RequestModel;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HDNXUdemyAPI.Controllers
{
    /// <summary>
    /// AuthenticationController
    /// </summary>
    [ApiVersion(VersionApi.Version1)]
    [Route(RouterControllerName.Authentication)]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationServices _authenticationServices;

        /// <summary>
        /// AuthenticationController
        /// </summary>
        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices ?? throw new ProjectException(nameof(_authenticationServices));
        }

        /// <summary>
        /// LoginWithGoogle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login-google")]
        public async Task<RepositoryModel<ResponeLogin>> LoginWithGoogle(LoginWithGoogle model)
        {
            RepositoryModel<ResponeLogin> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new ResponeLogin(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _authenticationServices.LoginWithGoogle(model, HttpContext);
            return result;
        }

        /// <summary>
        /// LoginWithFaceBook
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        [HttpPost("login-facebook")]
        public async Task<RepositoryModel<ResponeLogin>> LoginWithFaceBook(LoginWithGoogle credential)
        {
            RepositoryModel<ResponeLogin> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new ResponeLogin(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _authenticationServices.LoginWithFacebook(credential, HttpContext);
            return result;
        }

        /// <summary>
        /// RegisterUser
        /// </summary>
        /// <param name="email"></param>
        /// /// <param name="name"></param>
        /// /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("register/{name}/{phone}/{email}/{password}")]
        public async Task<RepositoryModel<bool>> RegisterUser(string name, string phone, string email, string password)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _authenticationServices.RegisterNormalUser(email, password, name, phone);
            return result;
        }

        /// <summary>
        /// LoginNormalAccount
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("login/{email}/{password}")]
        public async Task<RepositoryModel<ResponeLogin>> LoginNormalAccount(string email, string password)
        {
            RepositoryModel<ResponeLogin> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new ResponeLogin(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _authenticationServices.LoginNormalAccount(email, password, HttpContext);
            return result;
        }

        /// <summary>
        /// VerifyLinkRegister
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("verify-account/{id}/{email}")]
        public async Task<IActionResult> VerifyLinkRegister(long id, string email)
        {
            bool isUpdate = await _authenticationServices.IsActiveAccountAfterRegister(email, id);
            if (isUpdate)
            {
                return Redirect(ProjectConfig.LinkCompletedRegister ?? string.Empty);
            }
            else
            {
                return Redirect(ProjectConfig.LinkRegisterError ?? string.Empty);
            }
        }
    }
}