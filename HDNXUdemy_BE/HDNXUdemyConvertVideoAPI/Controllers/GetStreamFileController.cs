using Asp.Versioning;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace HDNXUdemyConvertVideoAPI.Controllers
{
    /// <summary>
    /// GetStreamFileController
    /// </summary>
    [ApiVersion(VersionApi.Version1)]
    [Route(RouterControllerName.GetVideoStreamFile)]
    public class GetStreamFileController : BaseController
    {
        private readonly IFileProvider _fileProvider;

        /// <summary>
        /// GetStreamFileController
        /// </summary>
        /// <param name="fileProvider"></param>
        public GetStreamFileController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("stream/{fileName}")]
        public IActionResult GetVideoStream(string fileName)
        {
            var filePath = Path.Combine($@"{ProjectConfig.DiskBaseForVideo}\StorageStreamVideo\{fileName}");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // File not found
            }

            var fileContent = System.IO.File.OpenRead(filePath ?? string.Empty);
            return File(fileContent, "application/octet-stream", enableRangeProcessing: true);
        }

        /// <summary>
        /// GetMp4VideoStream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("stream/mp4/{fileName}")]
        public async Task<IActionResult> GetMp4VideoStream(string fileName)
        {
            var filePath = _fileProvider.GetFileInfo($"StorageMainVideo/{fileName}");

            if (!filePath.Exists)
            {
                return NotFound(); // File not found
            }
            var memory = new MemoryStream();
            using (var file = new FileStream(filePath.PhysicalPath ?? string.Empty, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await file.CopyToAsync(memory);
            }
            return File(memory, "video/mp4", enableRangeProcessing: true);
        }
    }
}