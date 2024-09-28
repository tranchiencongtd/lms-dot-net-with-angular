using Asp.Versioning;
using HDNXUdemyData.Entities;
using HDNXUdemyData.Model;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.RequestModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HDNXUdemyAPI.Controllers
{
    /// <summary>
    /// PromotionOfCourseController
    /// </summary>
    [ApiVersion(VersionApi.Version1)]
    [Route(RouterControllerName.Promotion)]
    public class PromotionOfCourseController : BaseController
    {
        private readonly IStripeServices _stripeServices;

        /// <summary>
        /// PromotionOfCourseController
        /// </summary>
        /// <param name="stripeServices"></param>
        /// <exception cref="ProjectException"></exception>
        public PromotionOfCourseController(IStripeServices stripeServices)
        {
            _stripeServices = stripeServices ?? throw new ProjectException(nameof(_stripeServices));
        }

        /// <summary>
        /// CreatePromotionCoupon
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("coupon-promotion")]
        public async Task<RepositoryModel<bool>> CreatePromotionCoupon(CouponPromotionCode model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _stripeServices.CreateCouponForPromotion(model);
            return result;
        }

        /// <summary>
        /// DeleteCouponForPromotion
        /// </summary>
        /// <param name="stripeCouponId"></param>
        /// <returns></returns>
        [HttpPut("coupon-inactive/{stripeCouponId}")]
        public async Task<RepositoryModel<bool>> DeleteCouponForPromotion(string stripeCouponId)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _stripeServices.DeleteCouponForPromotion(stripeCouponId);
            return result;
        }

        /// <summary>
        /// CreateStripePromotionCode
        /// </summary>
        /// <param name="idCoupon"></param>
        /// <param name="promotionCode"></param>
        /// <returns></returns>
        [HttpPost("promotion-code/{idCoupon}/{promotionCode}")]
        public async Task<RepositoryModel<bool>> CreateStripePromotionCode(string idCoupon, string promotionCode)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _stripeServices.CreateStripePromotionCode(idCoupon, promotionCode);
            return result;
        }

        /// <summary>
        /// InactivePromotionCode
        /// </summary>
        /// <param name="promotionCodeId"></param>
        /// <returns></returns>
        [HttpPost("inactive-promotion-code/{promotionCodeId}")]
        public async Task<RepositoryModel<bool>> InactivePromotionCode(string promotionCodeId)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _stripeServices.InactivePromotionCode(promotionCodeId);
            return result;
        }

        /// <summary>
        /// GetListCouponActiveOnSystem
        /// </summary>
        /// <returns></returns>
        [HttpGet("coupon-promotion-code/{pageIndex}/{pageSize}")]
        public async Task<RepositoryModel<PagedResult<CouponModel>>> GetListCouponActiveOnSystem(int pageIndex, int pageSize)
        {
            RepositoryModel<PagedResult<CouponModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new PagedResult<CouponModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _stripeServices.GetListCouponActiveOnSystem(pageIndex, pageSize);
            return result;
        }

        /// <summary>
        /// UpdatePromotionCoupon
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("coupon-promotion")]
        public async Task<RepositoryModel<bool>> UpdatePromotionCoupon(CouponPromotionCode model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _stripeServices.UpdateCouponPromotionCode(model);
            return result;
        }

        /// <summary>
        /// GetListPromotions
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("promotion-codes/{pageIndex}/{pageSize}")]
        public async Task<RepositoryModel<PagedResult<PromotionCodeModel>>> GetListPromotions(int pageIndex, int pageSize)
        {
            RepositoryModel<PagedResult<PromotionCodeModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new PagedResult<PromotionCodeModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _stripeServices.GetListPromotions(pageIndex, pageSize);
            return result;
        }
    }
}