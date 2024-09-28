namespace HDNXUdemyModel.ResponModel
{
    public class ResponeLogin
    {
        public long? UserId { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string? Phone { get; set; }

        public int General { get; set; }

        public string? PictureUrl { get; set; }
        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? Token { get; set; }
    }
}