using DNXUdemyData.Entities;
using HDNXUdemyData.GenericRepository;
using HDNXUdemyData.IRepository;
using Microsoft.AspNetCore.Http;

namespace HDNXUdemyData.Repository
{
    public class ContentCourseDetailRepository : GenericRepository<ContentCourseDetailEntities>, IContentCourseDetailRepository
    {
        public ContentCourseDetailRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}