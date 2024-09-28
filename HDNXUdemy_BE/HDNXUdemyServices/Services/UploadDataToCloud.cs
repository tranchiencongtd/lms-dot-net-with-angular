using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Http;

namespace HDNXUdemyServices.Services
{
    public class UploadDataToCloud : IUploadDataToCloud
    {
        private Cloudinary _cloudinary;

        public UploadDataToCloud()
        {
            var accountCloud = new Account(ProjectConfig.CloudName, ProjectConfig.APIKey, ProjectConfig.APISecret);
            _cloudinary = new Cloudinary(accountCloud);
        }

        public async Task<ResponseUploadWithCloudinary> AddPhotoToCloudAsync(IFormFile formFile)
        {
            var accountCloud = new Account(ProjectConfig.CloudName, ProjectConfig.APIKey, ProjectConfig.APISecret);
            var uploadResult = new ImageUploadResult();
            _cloudinary = new Cloudinary(accountCloud);

            if (formFile.Length > 0)
            {
                using var stream = formFile.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(formFile.FileName, stream),
                    Transformation = new Transformation().Height(500).Crop("fill").Quality("auto")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            var resultData = new ResponseUploadWithCloudinary()
            {
                UrlPicture = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId
            };

            return resultData;
        }

        public async Task<ResponseUploadWithCloudinary> AddPhotoToCloudAsyncByBase64(string imagesBase64)
        {
            var accountCloud = new Account(ProjectConfig.CloudName, ProjectConfig.APIKey, ProjectConfig.APISecret);
            var uploadResult = new ImageUploadResult();
            _cloudinary = new Cloudinary(accountCloud);
            string dataUpload = $"data:image/jpeg;base64,{imagesBase64}";
            if (imagesBase64.Length > 0)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(@$"{dataUpload}"),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Format = "png"
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            var resultData = new ResponseUploadWithCloudinary()
            {
                UrlPicture = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId
            };

            return resultData;
        }

        public async Task<DeletionResult> DeletePhotoToCloudAsync(string publicId)
        {
            var accountCloud = new Account(ProjectConfig.CloudName, ProjectConfig.APIKey, ProjectConfig.APISecret);
            _cloudinary = new Cloudinary(accountCloud);
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}