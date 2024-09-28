using CloudinaryDotNet.Actions;
using HDNXUdemyModel.ResponModel;
using Microsoft.AspNetCore.Http;

namespace HDNXUdemyServices.IServices
{
    public interface IUploadDataToCloud
    {
        Task<ResponseUploadWithCloudinary> AddPhotoToCloudAsync(IFormFile formFile);

        Task<ResponseUploadWithCloudinary> AddPhotoToCloudAsyncByBase64(string imagesBase64);

        Task<DeletionResult> DeletePhotoToCloudAsync(string publicId);
    }
}