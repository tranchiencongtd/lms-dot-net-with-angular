using HDNXUdemyData.Entities;
using HDNXUdemyModel.Constant;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace HDNXUdemyData.SeedData
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDataDefault(this ModelBuilder modelBuilder)
        {
            LocalDateTime dateTime = LocalDateTime.FromDateTime(DateTime.UtcNow);
            modelBuilder.Entity<CategoryEntities>().HasData(
                new CategoryEntities { Id = 1, Name = "Công nghệ thông tin", CreateBy = 1, CreateDate = dateTime, Status = (int)EStatus.Active, UpdateBy = 1, UpdateDate = dateTime },
                new CategoryEntities { Id = 2, Name = "Chat GPT", CreateBy = 1, CreateDate = dateTime, Status = (int)EStatus.Active, UpdateBy = 1, UpdateDate = dateTime },
                new CategoryEntities { Id = 3, Name = "Khóa học cuộc sống", CreateBy = 1, CreateDate = dateTime, Status = (int)EStatus.Active, UpdateBy = 1, UpdateDate = dateTime }
    );
        }
    }
}