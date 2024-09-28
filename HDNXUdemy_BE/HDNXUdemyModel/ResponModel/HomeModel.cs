using HDNXUdemyModel.Model;

namespace HDNXUdemyModel.ResponModel
{
    public class HomeModel
    {
        public List<PartnerModel>? Partners { get; set; }

        public List<ListContentOfCourse>? ListContentData { get; set; }
    }

    public class ListContentOfCourse
    {
        public string? NameContent { get; set; }
        public List<CourseModel>? ListDataOfContent { get; set; }
    }
}