using AutoMapper;
using DNXUdemyData.Entities;
using HDNXUdemyData.Entities;
using HDNXUdemyData.Model;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyServices.CommonFunction;
using Stripe;

namespace HDNXUdemyAPI.Mapper
{
    /// <summary>
    /// ProjectMapper
    /// </summary>
    public class ProjectMapper : Profile
    {
        /// <summary>
        /// ProjectMapper
        /// </summary>
        public ProjectMapper()
        {
            CreateMap<NotificationEntities, NotificationModel>().ReverseMap();

            CreateMap<BookmarkCourseEntities, BookmarkCourseModel>().ReverseMap();
            CreateMap<CategoryEntities, CategoryModel>().ReverseMap();
            CreateMap<TheadQuestionCourseEntities, TheadQuestionCourseModel>().ReverseMap();
            CreateMap<ContentCourseEntities, ContentCourseModel>().ReverseMap();
            CreateMap<ContentCourseDetailEntities, ContentCourseDetailModel>()
                .ForMember(dest => dest.FileUploadUrlStream, opt => opt.MapFrom(x => $"{ProjectConfig.APIUrlGetVideoStream}{x.IdVideoUpload}.mp4"));
            CreateMap<ContentCourseDetailModel, ContentCourseDetailEntities>();
            CreateMap<BannerEntities, BannerModel>().ReverseMap();
            CreateMap<CourseEntities, CourseModel>().ReverseMap();
            CreateMap<InformationManualBankingEntities, InformationManualBankingModel>().ReverseMap();
            CreateMap<PurcharseCourseEntities, PurcharseCourseModel>().ReverseMap();
            CreateMap<StudentProcessEntities, StudentProcessModel>().ReverseMap();
            CreateMap<StudentPromotionEntities, StudentPromotionModel>().ReverseMap();
            CreateMap<SubCategoryEntities, SubCategoryModel>().ReverseMap();
            CreateMap<FileManagerEntities, FileManagerModel>().ReverseMap();
            CreateMap<SystemConfigEntities, SystemConfigModel>().ReverseMap();
            CreateMap<PartnerEntities, PartnerModel>().ReverseMap();
            CreateMap<UserEntities, UserModel>().ReverseMap();
            CreateMap<CourseEvaluationEntities, CourseEvaluationModel>().ReverseMap();

            CreateMap<ListContentWithDetailCourse, ContentCourseModel>().ReverseMap();
            CreateMap<ListContentWithDetailCourse, ContentCourseEntities>().ReverseMap();
            CreateMap<GetCourseWithDetailsContent, CourseEntities>().ReverseMap();
            CreateMap<ContentAndContentDetail, ContentCourseEntities>().ReverseMap();
            CreateMap<ResponeLogin, UserEntities>();
            CreateMap<UserEntities, ResponeLogin>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(x => ((ERoles)x.RoleId).GetEnumDescription()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.Id));
            CreateMap<PurcharseCourseDetailsEntities, PurcharseCourseDetailsModel>().ReverseMap();
            CreateMap<PagedResult<PurcharseCourseEntities>, PagedResult<PurcharseCourseModel>>().ReverseMap();
            CreateMap<Coupon, CouponModel>()
                .ForMember(dest => dest.StripeCouponId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Coupon, CouponEntities>()
                .ForMember(dest => dest.StripeCouponId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CouponEntities, CouponModel>().ReverseMap();
            CreateMap<PromotionCode, PromotionCodeModel>()
                .ForMember(dest => dest.StripePromotionCodeId, opt => opt.MapFrom(x => x.Id));
            CreateMap<PromotionCodeEntities, PromotionCodeModel>().ReverseMap();
            CreateMap<PagedResult<CouponEntities>, PagedResult<CouponModel>>().ReverseMap();
            CreateMap<CouponModel, Coupon>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CouponEntities, Coupon>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<PagedResult<PromotionCodeEntities>, PagedResult<PromotionCodeModel>>().ReverseMap();
        }
    }
}