using HDNXUdemyData.Entities;
using HDNXUdemyData.GenericRepository;
using HDNXUdemyData.IRepository;
using Microsoft.AspNetCore.Http;

namespace HDNXUdemyData.Repository
{
    public class RPurcharseCourseDetailsRepository : GenericRepository<PurcharseCourseDetailsEntities>, IRPPurcharseCourseDetailsRepository
    {
        public RPurcharseCourseDetailsRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}