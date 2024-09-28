using AutoMapper;
using HDNXUdemyData.Entities;
using HDNXUdemyData.IRepository;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.CommonFunction;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Xabe.FFmpeg;

namespace HDNXUdemyServices.Services
{
    public class UploadFileVideoToServer : IUploadFileVideoToServer
    {
        private readonly ILogServices<UploadFileVideoToServer> _logServices;
        private readonly IContentCourseDetailRepository _contentCourseDetailRepository;
        private readonly IFileManagerRepository _fileManagerRepository;
        private readonly IMapper _mapper;

        public UploadFileVideoToServer(ILogServices<UploadFileVideoToServer> logServices, IContentCourseDetailRepository contentCourseDetailRepository,
            IFileManagerRepository fileManagerRepository, IMapper mapper)
        {
            _logServices = logServices ?? throw new ProjectException(nameof(_logServices));
            _contentCourseDetailRepository = contentCourseDetailRepository ?? throw new ProjectException(nameof(_contentCourseDetailRepository));
            _fileManagerRepository = fileManagerRepository ?? throw new ProjectException(nameof(_fileManagerRepository));
            _mapper = mapper ?? throw new ProjectException(nameof(_mapper));
        }

        public async Task<ReturnUploadFile> UploadVideoFileToServer(IFormFile fileVideoUpload, string folderUpload, HttpRequest request)
        {
            var returnValue = new ReturnUploadFile();
            try
            {
                string fileName = string.Empty;
                string keyValue = Guid.NewGuid().ToString();
                string contentDispositionFileName = ContentDispositionHeaderValue.Parse(fileVideoUpload.ContentDisposition).FileName ?? string.Empty;
                _logServices.LogInformation(ETypeAction.Get, $"Upload video file to server with start patch : {fileVideoUpload} file path {contentDispositionFileName}");

                if (contentDispositionFileName?.LastIndexOf(".") != 0)
                {
                    string subStringContent = contentDispositionFileName!.Substring(contentDispositionFileName.LastIndexOf(".") + 1);
                    fileName = $"{keyValue}.{subStringContent.Trim('"')}";
                }
                else
                {
                    fileName = $"{keyValue}.{contentDispositionFileName.Trim('"')}";
                }

                string folder = $@"{ProjectConfig.DiskBaseForVideo}\{folderUpload}";
                _logServices.LogInformation(ETypeAction.Get, $"Upload video file to server with start patch : {fileVideoUpload} file path {folder}");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string filePath = Path.Combine(folder, fileName);
                using (FileStream fs = File.Create(filePath))
                {
                    await fileVideoUpload.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                returnValue.FileName = await ConvertVideoToStreamFile(fileName);
                returnValue.IsUpload = true;
                returnValue.KeyOfFile = keyValue;
                returnValue.FileSize = $"{Math.Round(fileVideoUpload.Length / 1024.0 / 1024.0, 2)} MB";
                returnValue.FileUploadUrlStream = $"{request.Scheme}://{request.Host}/api/v1/get-video-stream/stream/{keyValue}.m3u8";
                return returnValue;
            }
            catch (Exception ex)
            {
                returnValue.FileName = string.Empty;
                returnValue.IsUpload = false;
                _logServices.LogError(ETypeAction.Get, $"Upload video file to server with start patch : {fileVideoUpload} with error {ex.Message.ToString()}");
                return returnValue;
            }
        }

        public async Task<ReturnUploadFile> UploadVideoMp4FileToServer(IFormFile fileVideoUpload, string folderUpload, HttpRequest request)
        {
            var returnValue = new ReturnUploadFile();
            try
            {
                string fileName = string.Empty;
                string keyValue = Guid.NewGuid().ToString();
                string contentDispositionFileName = ContentDispositionHeaderValue.Parse(fileVideoUpload.ContentDisposition).FileName ?? string.Empty;
                _logServices.LogInformation(ETypeAction.Get, $"Upload video file to server with start patch : {fileVideoUpload} file path {contentDispositionFileName}");

                if (contentDispositionFileName?.LastIndexOf(".") != 0)
                {
                    string subStringContent = contentDispositionFileName!.Substring(contentDispositionFileName.LastIndexOf(".") + 1);
                    fileName = $"{keyValue}.{subStringContent.Trim('"')}";
                }
                else
                {
                    fileName = $"{keyValue}.{contentDispositionFileName.Trim('"')}";
                }

                string folder = $@"{ProjectConfig.DiskBaseForVideo}\{folderUpload}";
                _logServices.LogInformation(ETypeAction.Get, $"Upload video file to server with start patch : {fileVideoUpload} file path {folder}");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string filePath = Path.Combine(folder, fileName);
                using (FileStream fs = File.Create(filePath))
                {
                    await fileVideoUpload.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                returnValue.FileName = fileName;
                returnValue.IsUpload = true;
                returnValue.KeyOfFile = keyValue;
                returnValue.FileSize = $"{Math.Round(fileVideoUpload.Length / 1024.0 / 1024.0, 2)} MB";
                returnValue.FileUploadUrlStream = $"{request.Scheme}://{request.Host}/api/v1/get-video-stream/stream/mp4/{fileName}";
                return returnValue;
            }
            catch (Exception ex)
            {
                returnValue.FileName = string.Empty;
                returnValue.IsUpload = false;
                _logServices.LogError(ETypeAction.Get, $"Upload video file to server with start patch : {fileVideoUpload} with error {ex.Message.ToString()}");
                return returnValue;
            }
        }

        private async Task<string> ConvertVideoToStreamFile(string fileNameUpload)
        {
            string folderUploadVideo = $@"{ProjectConfig.DiskBaseForVideo}\{ProjectConfig.StorageMainVideo}";
            string folderUploadStreamVideo = $@"{ProjectConfig.DiskBaseForVideo}\{ProjectConfig.StorageStreamVideo}";
            string returnFileName = string.Empty;

            if (!Directory.Exists(folderUploadStreamVideo))
            {
                Directory.CreateDirectory(folderUploadStreamVideo);
            }

            // 1. Get file upload from client
            var getFileVideo = Directory.GetFiles(folderUploadVideo, fileNameUpload);

            foreach (var fileVideo in getFileVideo)
            {
                try
                {
                    returnFileName = fileNameUpload.Substring(0, fileNameUpload.IndexOf("."));
                    var outputMainStreamFile = Path.Combine(folderUploadStreamVideo, $"{returnFileName}.m3u8");
                    var outputStreamFileTS = Path.Combine(folderUploadStreamVideo, $"{returnFileName}_%03d.ts");
                    IMediaInfo info = await FFmpeg.GetMediaInfo(fileVideo);
                    var data = await FFmpeg.Conversions.New()
                    .AddStream(info.Streams)
                    .AddParameter("-codec: copy -start_number 0")
                    .AddParameter($"-hls_time 10 -hls_list_size 0 -f hls -hls_segment_filename \"{outputStreamFileTS}\"")
                    .SetOutput(outputMainStreamFile)
                    .UseMultiThread(true).
                    Start();

                    //File.Delete(fileVideo);
                    return $"{returnFileName}.m3u8";
                }
                catch (Exception ex)
                {
                    _logServices.LogError(ETypeAction.Get, $"Convert video is not working with filename {fileVideo} with error {ex.Message}");
                    return returnFileName;
                }
            }
            return returnFileName;
        }

        public async Task<bool> CreateFileSoftware(FileManagerModel model)
        {
            var dataInsert = _mapper.Map<FileManagerEntities>(model);
            dataInsert.Icon = ((EFileTypeUpload)Enum.Parse(typeof(EFileTypeUpload), model.ExtendFile ?? string.Empty, true)).GetEnumDescription();
            return await _fileManagerRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusSoftware(long id, FileManagerModel model)
        {
            var getData = await _fileManagerRepository.GetByIdAsync(id) ?? new FileManagerEntities();
            getData.Status = model.Status;
            return await _fileManagerRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationSoftware(long id, FileManagerModel model)
        {
            var getData = await _fileManagerRepository.GetByIdAsync(id) ?? new FileManagerEntities();
            getData.Status = model.Status;
            getData.FileName = model.FileName;
            getData.ActualNameFile = model.ActualNameFile;
            getData.FileUrl = model.FileUrl;
            getData.IsFree = model.IsFree;
            getData.ExtendFile = model.ExtendFile;
            getData.KeyOfFile = model.KeyOfFile;
            return await _fileManagerRepository.UpdateAsync(getData);
        }

        public async Task<List<FileManagerModel>> GetListFileSoftware(HttpRequest request)
        {
            var getData = await _fileManagerRepository.GetAllAsync();
            var returnValue = _mapper.Map<List<FileManagerModel>>(getData);
            foreach (var item in returnValue)
            {
                item.FileUrl = $"{request.Scheme}://{request.Host}/{ProjectConfig.UploadSoftWareAndFile}/{item.FileUrl}";
            }

            return returnValue;
        }

        public async Task<FileManagerModel> GetFileSoftware(long id)
        {
            var getData = await _fileManagerRepository.GetByIdAsync(id);
            return _mapper.Map<FileManagerModel>(getData);
        }
    }
}