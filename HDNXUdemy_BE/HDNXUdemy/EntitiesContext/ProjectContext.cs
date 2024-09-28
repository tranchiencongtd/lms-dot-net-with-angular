using DNXUdemyData.Entities;
using HDNXUdemyData.Entities;
using HDNXUdemyData.SeedData;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HDNXUdemyData.EntitiesContext
{
    public class ProjectContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ProjectConfig.ConnectionString ?? "", builder => builder.UseNodaTime());
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        public virtual DbSet<NotificationEntities>? Notifications { get; set; }
        public virtual DbSet<BookmarkCourseEntities>? BookmarkCourses { get; set; }
        public virtual DbSet<BannerEntities>? Banners { get; set; }
        public virtual DbSet<CategoryEntities>? Categories { get; set; }
        public virtual DbSet<TheadQuestionCourseEntities>? TheadQuestionCourses { get; set; }
        public virtual DbSet<ContentCourseEntities>? ContentCourses { get; set; }
        public virtual DbSet<ContentCourseDetailEntities>? ContentCourseDetails { get; set; }
        public virtual DbSet<CourseEntities>? Courses { get; set; }
        public virtual DbSet<InformationManualBankingEntities>? InformationManualBankings { get; set; }
        public virtual DbSet<PurcharseCourseEntities>? PurcharseCourses { get; set; }
        public virtual DbSet<PurcharseCourseDetailsEntities>? PurcharseCourseDetails { get; set; }
        public virtual DbSet<UserEntities>? Users { get; set; }
        public virtual DbSet<StudentProcessEntities>? StudentProcess { get; set; }
        public virtual DbSet<StudentPromotionEntities>? StudentPromotions { get; set; }
        public virtual DbSet<SubCategoryEntities>? SubCategories { get; set; }
        public virtual DbSet<FileManagerEntities>? FileManagers { get; set; }
        public virtual DbSet<SystemConfigEntities>? SystemConfigs { get; set; }
        public virtual DbSet<PartnerEntities>? Partners { get; set; }
        public virtual DbSet<CourseEvaluationEntities>? CourseEvaluations { get; set; }
        public virtual DbSet<DetailTheadQuestionCourseEntities>? DetailTheadQuestionCourses { get; set; }

        public virtual DbSet<CouponEntities>? Coupons { get; set; }
        public virtual DbSet<PromotionCodeEntities>? PromotionCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(ProjectConfig.DefaultSchema);
            modelBuilder.Entity<NotificationEntities>().ToTable("Notifications").HasKey(x => x.Id);
            modelBuilder.Entity<BookmarkCourseEntities>().ToTable("BookmarkCourses").HasKey(x => x.Id);
            modelBuilder.Entity<BannerEntities>().ToTable("Banners").HasKey(x => x.Id);
            modelBuilder.Entity<CategoryEntities>().ToTable("Categories").HasKey(x => x.Id);
            modelBuilder.Entity<TheadQuestionCourseEntities>().ToTable("TheadQuestionCourses").HasKey(x => x.Id);
            modelBuilder.Entity<ContentCourseEntities>().ToTable("ContentCourses").HasKey(x => x.Id);
            modelBuilder.Entity<ContentCourseDetailEntities>().ToTable("ContentCourseDetails").HasKey(x => x.Id);
            modelBuilder.Entity<InformationManualBankingEntities>().ToTable("InformationManualBankings").HasKey(x => x.Id);
            modelBuilder.Entity<PurcharseCourseEntities>().ToTable("PurcharseCourses").HasKey(x => x.Id);
            modelBuilder.Entity<PurcharseCourseDetailsEntities>().ToTable("PurcharseCourseDetails").HasKey(x => x.Id);
            modelBuilder.Entity<UserEntities>().ToTable("Users").HasKey(x => x.Id);
            modelBuilder.Entity<StudentProcessEntities>().ToTable("StudentProcess").HasKey(x => x.Id);
            modelBuilder.Entity<StudentPromotionEntities>().ToTable("StudentPromotions").HasKey(x => x.Id);
            modelBuilder.Entity<SubCategoryEntities>().ToTable("SubCategories").HasKey(x => x.Id);
            modelBuilder.Entity<FileManagerEntities>().ToTable("FileManagers").HasKey(x => x.Id);
            modelBuilder.Entity<SystemConfigEntities>().ToTable("SystemConfigs").HasKey(x => x.Id);
            modelBuilder.Entity<PartnerEntities>().ToTable("Partners").HasKey(x => x.Id);
            modelBuilder.Entity<CourseEvaluationEntities>().ToTable("CourseEvaluations").HasKey(x => x.Id);
            modelBuilder.Entity<DetailTheadQuestionCourseEntities>().ToTable("DetailTheadQuestionCourses").HasKey(x => x.Id);
            modelBuilder.Entity<CouponEntities>().ToTable("Coupons").HasKey(x => x.Id);
            modelBuilder.Entity<PromotionCodeEntities>().ToTable("PromotionCodes").HasKey(x => x.Id);
            modelBuilder.SeedDataDefault();
        }

        public override int SaveChanges()
        {
            LocalDateTime dateNow = LocalDateTime.FromDateTime(DateTime.UtcNow);
            var errorList = new List<ValidationResult>();
            int idCurrentUser = _httpContextAccessor.HttpContext == null ? 0 : int.Parse(_httpContextAccessor.HttpContext.User.Claims
                                            .Where(x => x.Type == "user-id").FirstOrDefault()?.Value ?? "0");

            var entries = ChangeTracker.Entries()
         .Where(p => p.State == EntityState.Added ||
             p.State == EntityState.Modified).ToList();

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.CreateDate = itemBase.UpdateDate = dateNow;
                        itemBase.CreateBy = idCurrentUser;
                        itemBase.UpdateBy = idCurrentUser;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.UpdateDate = dateNow;
                        itemBase.UpdateBy = idCurrentUser;
                    }
                }

                Validator.TryValidateObject(entity, new ValidationContext(entity), errorList);
            }

            if (errorList.Any())
            {
                throw new Exception(string.Join(", ", errorList.Select(p => p.ErrorMessage)).Trim());
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            LocalDateTime dateNow = LocalDateTime.FromDateTime(DateTime.UtcNow);
            var errorList = new List<ValidationResult>();
            int idCurrentUser = _httpContextAccessor.HttpContext == null ? 0 : int.Parse(_httpContextAccessor.HttpContext.User.Claims
                                            .Where(x => x.Type == "user-id").FirstOrDefault()?.Value ?? "0");

            var entries = ChangeTracker.Entries()
         .Where(p => p.State == EntityState.Added ||
             p.State == EntityState.Modified).ToList();

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.CreateDate = itemBase.UpdateDate = dateNow;
                        itemBase.CreateBy = idCurrentUser;
                        itemBase.UpdateBy = idCurrentUser;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.UpdateDate = dateNow;
                        itemBase.UpdateBy = idCurrentUser;
                    }
                }

                Validator.TryValidateObject(entity, new ValidationContext(entity), errorList);
            }

            if (errorList.Any())
            {
                throw new Exception(string.Join(", ", errorList.Select(p => p.ErrorMessage)).Trim());
            }

            return base.SaveChangesAsync();
        }
    }
}