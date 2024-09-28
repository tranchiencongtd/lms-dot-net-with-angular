using HDNXUdemyData.Entities;
using HDNXUdemyData.GenericRepository;
using HDNXUdemyData.IRepository;
using Microsoft.AspNetCore.Http;

namespace HDNXUdemyData.Repository
{
    public class RCourseEvaluationRepository : GenericRepository<CourseEvaluationEntities>, IRCourseEvaluationRepository
    {
        public RCourseEvaluationRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}