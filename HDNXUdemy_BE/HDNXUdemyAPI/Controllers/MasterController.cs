using Asp.Versioning;
using HDNXUdemyData.Model;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HDNXUdemyAPI.Controllers
{
    /// <summary>
    /// MasterController
    /// </summary>
    [ApiVersion(VersionApi.Version1)]
    [Route(RouterControllerName.MasterData)]
    public class MasterController : BaseController
    {
        private readonly IMasterDataServices _masterDataServices;
        private readonly ISendEmailSMSServices _sendEmailServices;

        /// <summary>
        /// MasterController
        /// </summary>
        /// <param name="sendEmailServices"></param>
        /// <param name="masterDataServices"></param>
        /// <exception cref="ProjectException"></exception>
        public MasterController(IMasterDataServices masterDataServices, ISendEmailSMSServices sendEmailServices)
        {
            _masterDataServices = masterDataServices ?? throw new ProjectException(nameof(_masterDataServices));
            _sendEmailServices = sendEmailServices ?? throw new ProjectException(nameof(_sendEmailServices));
        }

        /// <summary>
        /// CreateCategory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("category")]
        public async Task<RepositoryModel<bool>> CreateCategory(CategoryModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.CreateCategory(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusCategory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("category/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusCategory(long id, CategoryModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateStatusCategory(id, model);
            return result;
        }

        /// <summary>
        /// DeleteCategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("category/delete/{id}")]
        public async Task<RepositoryModel<bool>> DeleteCategory(long id)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.DeleteCategory(id);
            return result;
        }

        /// <summary>
        /// UpdateInformationCategory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("category")]
        public async Task<RepositoryModel<bool>> UpdateInformationCategory(CategoryModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateInformationCategory(model.Id ?? 0, model);
            return result;
        }

        /// <summary>
        /// GetListCategory
        /// </summary>
        /// <returns></returns>
        [HttpGet("category")]
        public async Task<RepositoryModel<List<CategoryModel>>> GetListCategory()
        {
            RepositoryModel<List<CategoryModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<CategoryModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.GetListCategory();
            return result;
        }

        /// <summary>
        /// CreateBanner
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("banner")]
        public async Task<RepositoryModel<bool>> CreateBanner(BannerModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.CreateBanner(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusBanner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("banner/status/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusBanner(long id, BannerModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateStatusBanner(id, model);
            return result;
        }

        /// <summary>
        /// UpdateInformationBanner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("banner/{id}")]
        public async Task<RepositoryModel<bool>> UpdateInformationBanner(long id, BannerModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateInformationBanner(id, model);
            return result;
        }

        /// <summary>
        /// GetListBanner
        /// </summary>
        /// <returns></returns>
        [HttpGet("banner")]
        public async Task<RepositoryModel<List<BannerModel>>> GetListBanner()
        {
            RepositoryModel<List<BannerModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<BannerModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.GetListBanner();
            return result;
        }

        /// <summary>
        /// CreateInformationManualBanking
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("banking-information")]
        public async Task<RepositoryModel<bool>> CreateInformationManualBanking(InformationManualBankingModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.CreateInformationManualBanking(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusInformationManualBanking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("banking-information/status/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusInformationManualBanking(long id, InformationManualBankingModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateStatusInformationManualBanking(id, model);
            return result;
        }

        /// <summary>
        /// UpdateInformationManualBanking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("banking-information/{id}")]
        public async Task<RepositoryModel<bool>> UpdateInformationManualBanking(long id, InformationManualBankingModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateInformationManualBanking(id, model);
            return result;
        }

        /// <summary>
        /// GetListInformationManualBanking
        /// </summary>
        /// <returns></returns>
        [HttpGet("banking-information")]
        public async Task<RepositoryModel<InformationManualBankingModel>> GetListInformationManualBanking()
        {
            RepositoryModel<InformationManualBankingModel> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new InformationManualBankingModel(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.GetInformationManualBanking();
            return result;
        }

        /// <summary>
        /// CreateSubCategory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("sub-category")]
        public async Task<RepositoryModel<bool>> CreateSubCategory(SubCategoryModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.CreateSubCategory(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusSubCategory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("sub-category/status/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusSubCategory(long id, SubCategoryModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateStatusSubCategory(id, model);
            return result;
        }

        /// <summary>
        /// UpdateInformationSubCategory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("sub-category/{id}")]
        public async Task<RepositoryModel<bool>> UpdateInformationSubCategory(long id, SubCategoryModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateInformationSubCategory(id, model);
            return result;
        }

        /// <summary>
        /// GetListSubCategory
        /// </summary>
        /// <returns></returns>
        [HttpGet("sub-category")]
        public async Task<RepositoryModel<List<SubCategoryModel>>> GetListSubCategory()
        {
            RepositoryModel<List<SubCategoryModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<SubCategoryModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.GetListSubCategory();
            return result;
        }

        /// <summary>
        /// GetSubCategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("sub-category/{id}")]
        public async Task<RepositoryModel<SubCategoryModel>> GetSubCategory(long id)
        {
            RepositoryModel<SubCategoryModel> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new SubCategoryModel(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.GetSubCategory(id);
            return result;
        }

        /// <summary>
        /// TestSendEmail
        /// </summary>
        /// <returns></returns>
        [HttpGet("test-send-email")]
        public async Task<RepositoryModel<bool>> TestSendEmail()
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _sendEmailServices.SendEmailToSingleEmail("anglesen1180@gmail.com", "Test Email 313", "Test sendEmail 13312");
            return result;
        }

        /// <summary>
        /// GetListFileTypeUpload
        /// </summary>
        /// <returns></returns>
        [HttpGet("list-file-type-upload")]
        public RepositoryModel<List<ListTypeFileUpload>> GetListFileTypeUpload()
        {
            RepositoryModel<List<ListTypeFileUpload>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<ListTypeFileUpload>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = _masterDataServices.GetListFileTypeUpload();
            return result;
        }

        /// <summary>
        /// CreateConfigSystem
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("config-system")]
        public async Task<RepositoryModel<bool>> CreateConfigSystem(SystemConfigModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.CreateConfigSystem(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusConfigSystem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("config-system/status/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusConfigSystem(long id, SystemConfigModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateStatusConfigSystem(id, model);
            return result;
        }

        /// <summary>
        /// UpdateInformationConfigSystem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("config-system/{id}")]
        public async Task<RepositoryModel<bool>> UpdateInformationConfigSystem(long id, SystemConfigModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.UpdateInformationConfigSystem(id, model);
            return result;
        }

        /// <summary>
        /// GetListConfigSystem
        /// </summary>
        /// <returns></returns>
        [HttpGet("config-system")]
        public async Task<RepositoryModel<List<SystemConfigModel>>> GetListConfigSystem()
        {
            RepositoryModel<List<SystemConfigModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<SystemConfigModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.GetListConfigSystem();
            return result;
        }

        /// <summary>
        /// GetConfigSystem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("config-system/{id}")]
        public async Task<RepositoryModel<SystemConfigModel>> GetConfigSystem(long id)
        {
            RepositoryModel<SystemConfigModel> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new SystemConfigModel(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _masterDataServices.GetConfigSystem(id);
            return result;
        }

        /// <summary>
        /// GetListOfFileTypeUpload
        /// </summary>
        /// <returns></returns>
        [HttpGet("list-of-file-type-upload")]
        public RepositoryModel<List<ListTypeFileUpload>> GetListOfFileTypeUpload()
        {
            RepositoryModel<List<ListTypeFileUpload>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<ListTypeFileUpload>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = _masterDataServices.GetListOfFileTypeUpload();
            return result;
        }
    }
}