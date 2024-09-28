using Asp.Versioning;
using CloudinaryDotNet.Actions;
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
    /// UploadToCloudController
    /// </summary>
    [ApiVersion(VersionApi.Version1)]
    [Route(RouterControllerName.UploadToCloud)]
    public class UploadToCloudController : BaseController
    {
        private readonly IUploadDataToCloud _uploadDataToCloud;
        private readonly IUploadFileVideoToServer _uploadFileVideoToServer;

        /// <summary>
        /// UploadToCloudController
        /// </summary>
        public UploadToCloudController(IUploadDataToCloud uploadDataToCloud, IUploadFileVideoToServer uploadFileVideoToServer)
        {
            _uploadDataToCloud = uploadDataToCloud ?? throw new ProjectException(nameof(_uploadDataToCloud));
            _uploadFileVideoToServer = uploadFileVideoToServer ?? throw new ProjectException(nameof(_uploadFileVideoToServer));
        }

        /// <summary>
        /// AddPhotoTocloudinaryAsync
        /// </summary>
        /// <param name="filePicture"></param>
        /// <returns></returns>
        [HttpPost("cloudinary")]
        public async Task<RepositoryModel<ResponseUploadWithCloudinary>> AddPhotoToCloudinaryAsync(IFormFile filePicture)
        {
            RepositoryModel<ResponseUploadWithCloudinary> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new ResponseUploadWithCloudinary(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _uploadDataToCloud.AddPhotoToCloudAsync(filePicture);
            return result;
        }

        /// <summary>
        /// DeletePhotoToCloudinarAsync
        /// </summary>
        /// <param name="publicId"></param>
        /// <returns></returns>
        [HttpDelete("cloudinary/{publicId}")]
        public async Task<RepositoryModel<DeletionResult>> DeletePhotoToCloudinarAsync(string publicId)
        {
            RepositoryModel<DeletionResult> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new DeletionResult(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _uploadDataToCloud.DeletePhotoToCloudAsync(publicId);
            return result;
        }

        /// <summary>
        /// AddPhotoToCloudinaryRequestAsync
        /// </summary>
        /// <returns></returns>
        [HttpPost("cloudinary-request")]
        public async Task<RepositoryModel<ResponseUploadWithCloudinary>> AddPhotoToCloudinaryRequestAsync()
        {
            RepositoryModel<ResponseUploadWithCloudinary> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new ResponseUploadWithCloudinary(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };
            var files = Request.Form.Files;
            result.Data = await _uploadDataToCloud.AddPhotoToCloudAsync(files[0]);
            return result;
        }

        /// <summary>
        /// CreateCourse
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("file-software")]
        public async Task<RepositoryModel<bool>> CreateFileSoftware(FileManagerModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _uploadFileVideoToServer.CreateFileSoftware(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusCourse
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("file-software/status/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusSoftware(long id, FileManagerModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _uploadFileVideoToServer.UpdateStatusSoftware(id, model);
            return result;
        }

        /// <summary>
        /// UpdateInformationCourse
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("file-software/{id}")]
        public async Task<RepositoryModel<bool>> UpdateInformationSoftware(long id, FileManagerModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _uploadFileVideoToServer.UpdateInformationSoftware(id, model);
            return result;
        }

        /// <summary>
        /// GetListStudentProcess
        /// </summary>
        /// <returns></returns>
        [HttpGet("file-software")]
        public async Task<RepositoryModel<List<FileManagerModel>>> GetListFileSoftware()
        {
            RepositoryModel<List<FileManagerModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<FileManagerModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _uploadFileVideoToServer.GetListFileSoftware(Request);
            return result;
        }

        /// <summary>
        /// GetStudentProcess
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("file-software/{id}")]
        public async Task<RepositoryModel<FileManagerModel>> GetFileSoftware(long id)
        {
            RepositoryModel<FileManagerModel> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new FileManagerModel(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _uploadFileVideoToServer.GetFileSoftware(id);
            return result;
        }
    }
}