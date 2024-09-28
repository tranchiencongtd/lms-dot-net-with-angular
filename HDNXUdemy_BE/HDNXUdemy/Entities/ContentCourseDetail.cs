using HDNXUdemyData.Entities;

namespace DNXUdemyData.Entities
{
    public class ContentCourseDetailEntities : BaseEntities
    {
        public long IdContent { get; set; }

        public string? NameSubContent { get; set; }

        public string? TimeOfContent { get; set; }

        public bool IsLearningFree { get; set; }

        public string? IdVideoUpload { get; set; }

        public string? FileNameVideo { get; set; }

        public bool IsFinishConvert { get; set; }
    }
}