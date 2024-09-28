using HDNXUdemyData.Entities;
using HDNXUdemyData.Model;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.RequestModel;
using HDNXUdemyModel.ResponModel;

namespace HDNXUdemyServices.IServices
{
    public interface IStripeServices
    {
        Task<CheckoutSessionResponse> CreateCheckoutSession(PurcharseCourseModel model);

        Task<bool> CreateCouponForPromotion(CouponPromotionCode model);

        Task<bool> DeleteCouponForPromotion(string stripeCouponId);

        Task<bool> CreateStripePromotionCode(string idCoupon, string promotionCode);

        Task<bool> InactivePromotionCode(string promotionCodeId);

        Task<PagedResult<CouponModel>> GetListCouponActiveOnSystem(int pageIndex, int pageSize);

        Task<bool> UpdateCouponPromotionCode(CouponPromotionCode model);

        Task<PagedResult<PromotionCodeModel>> GetListPromotions(int pageIndex, int pageSize);
    }
}