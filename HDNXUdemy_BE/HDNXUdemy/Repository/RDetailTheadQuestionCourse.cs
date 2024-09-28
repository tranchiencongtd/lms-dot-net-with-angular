using HDNXUdemyData.Entities;
using HDNXUdemyData.GenericRepository;
using HDNXUdemyData.IRepository;
using Microsoft.AspNetCore.Http;

namespace HDNXUdemyData.Repository
{
    public class RDetailTheadQuestionCourseRepository : GenericRepository<DetailTheadQuestionCourseEntities>, IRDetailTheadQuestionCourseRepository
    {
        public RDetailTheadQuestionCourseRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}