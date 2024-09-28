using Asp.Versioning;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HDNXUdemyConvertVideoAPI.Controllers
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
        /// LoginNormalAccount
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("login")]
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
        /// GeneralToken
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("general-token/{email}")]
        public async Task<RepositoryModel<ResponeLogin>> GeneralToken(string email)
        {
            RepositoryModel<ResponeLogin> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new ResponeLogin(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _authenticationServices.GeneralToken(email, HttpContext);
            return result;
        }
    }
}