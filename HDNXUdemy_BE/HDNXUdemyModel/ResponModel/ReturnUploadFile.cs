namespace HDNXUdemyModel.ResponModel
{
    public class ReturnUploadFile
    {
        public bool IsUpload { get; set; } = false;

        public string? FileName { get; set; }

        public string? KeyOfFile { get; set; }

        public string? FileSize { get; set; }

        public string? FileUploadUrlStream { get; set; }
    }
}