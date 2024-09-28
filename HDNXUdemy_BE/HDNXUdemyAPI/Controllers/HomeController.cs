using Asp.Versioning;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HDNXUdemyAPI.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    [ApiVersion(VersionApi.Version1)]
    [Route(RouterControllerName.Home)]
    public class HomeController : BaseController
    {
        private readonly IHomeServices _homeServices;

        /// <summary>
        /// HomeController
        /// </summary>
        /// <param name="homeServices"></param>
        public HomeController(IHomeServices homeServices)
        {
            _homeServices = homeServices ?? throw new ProjectException(nameof(_homeServices));
        }

        /// <summary>
        /// GetDataForHome
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<RepositoryModel<HomeModel>> GetDataForHome()
        {
            RepositoryModel<HomeModel> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new HomeModel(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _homeServices.GetDataForHome(1);
            return result;
        }
    }
}