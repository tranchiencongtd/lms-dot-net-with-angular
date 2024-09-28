namespace HDNXUdemyData.Entities
{
    public class FileManagerEntities : BaseEntities
    {
        public string? FileName { get; set; }

        public string? FileType { get; set; }

        public string? FileUrl { get; set; }

        public string? ActualNameFile { get; set; }

        public bool IsFree { get; set; }

        public string? ExtendFile { get; set; }

        public string? KeyOfFile { get; set; }

        public string? Icon { get; set; }

        public string? FileSize { get; set; }
    }
}