using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class UserModel : BaseModel
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public int General { get; set; }

        public string? PictureUrl { get; set; }

        public string? PublicId { get; set; }

        public int RoleId { get; set; }

        public int TypeLogin { get; set; }

        public bool IsRequestTeacher { get; set; }

        public int TotalCourseRegister { get; set; }
        public bool? IsActive { get; set; }
    }
}