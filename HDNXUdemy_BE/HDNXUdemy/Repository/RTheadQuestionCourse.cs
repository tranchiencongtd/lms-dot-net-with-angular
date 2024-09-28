using HDNXUdemyData.Entities;
using HDNXUdemyData.GenericRepository;
using HDNXUdemyData.IRepository;
using Microsoft.AspNetCore.Http;

namespace HDNXUdemyData.Repository
{
    public class RTheadQuestionCourseRepository : GenericRepository<TheadQuestionCourseEntities>, IRTheadQuestionCourseRepository
    {
        public RTheadQuestionCourseRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}