using AutoMapper;
using HDNXUdemyData.Entities;
using HDNXUdemyData.IRepository;
using HDNXUdemyData.Model;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.RequestModel;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.CommonFunction;
using HDNXUdemyServices.IServices;
using Microsoft.Extensions.Caching.Distributed;
using NodaTime;
using Stripe;
using Stripe.Checkout;

namespace HDNXUdemyServices.Services
{
    public class StripeServices : IStripeServices
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IRCouponRepository _couponRepository;
        private readonly IRPromotionCodeRepository _promotionCodeRepository;
        private readonly IDistributedCache _distributedCache;

        public StripeServices(ICourseRepository courseRepository, IMapper mapper, IRCouponRepository couponRepository, IRPromotionCodeRepository promotionCodeRepository,
            IDistributedCache distributedCache)
        {
            _courseRepository = courseRepository ?? throw new ProjectException(nameof(_courseRepository));
            _mapper = mapper ?? throw new ProjectException(nameof(_mapper));
            _couponRepository = couponRepository ?? throw new ProjectException(nameof(_couponRepository));
            _promotionCodeRepository = promotionCodeRepository ?? throw new ProjectException(nameof(_promotionCodeRepository));
            _distributedCache = distributedCache ?? throw new ProjectException(nameof(_distributedCache));
        }

        public async Task<CheckoutSessionResponse> CreateCheckoutSession(PurcharseCourseModel model)
        {
            var listSessionLineItemOption = new List<SessionLineItemOptions>();
            var returnValue = new CheckoutSessionResponse();
            foreach (var item in model.ListPurchaseCourseDetails)
            {
                var dataOfCoures = await _courseRepository.GetByIdAsync(item.IdCourse);
                var itemSessionItemOption = new SessionLineItemOptions()
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmountDecimal = dataOfCoures.PriceOfCourse,
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = dataOfCoures.Title,
                            Images = new List<string> { dataOfCoures.PictureUrl },
                        }
                    },
                };
                listSessionLineItemOption.Add(itemSessionItemOption);
            }
            var options = new SessionCreateOptions
            {
                SuccessUrl = ProjectConfig.StripePaymentSuccessLink,
                CancelUrl = ProjectConfig.StripePaymentUnSuccessLink,
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                Mode = "payment",
                LineItems = listSessionLineItemOption,
                ClientReferenceId = model.PurcharseCode.ToString(),
            };

            try
            {
                var stripeServices = new SessionService();
                var session = await stripeServices.CreateAsync(options);
                returnValue.SessionId = session.Id;
                await DistributedCacheRedis.SetDataToDistributedCache(_distributedCache, model.PurcharseCode.ToString(), model);
            }
            catch (StripeException ex)
            {
                throw new ProjectException(ex.InnerException?.Message ?? string.Empty, ex);
            }
            return returnValue;
        }

        public async Task<bool> CreateStripePromotionCode(string idCoupon, string promotionCode)
        {
            try
            {
                var options = new PromotionCodeCreateOptions
                {
                    Coupon = idCoupon,
                    Code = promotionCode,
                    Active = true,
                };
                var service = new PromotionCodeService();
                var stripeCreatePromotionCode = await service.CreateAsync(options);
                var mapperStripeToModel = _mapper.Map<PromotionCodeModel>(stripeCreatePromotionCode);
                var dataInsert = _mapper.Map<PromotionCodeEntities>(mapperStripeToModel);
                return await _promotionCodeRepository.AddAsync(dataInsert);
            }
            catch (StripeException ex)
            {
                throw new ProjectException(ex.InnerException?.Message ?? string.Empty, ex);
            }
        }

        public async Task<bool> CreateCouponForPromotion(CouponPromotionCode model)
        {
            var options = new CouponCreateOptions
            {
                Duration = model.TypeDuration,
                PercentOff = model.PercentOff,
                Currency = model.Currency,
            };
            try
            {
                var dataInsert = new CouponEntities();
                if (model.Status == (int)EStatus.Active)
                {
                    var service = new CouponService();
                    var stripeCreateCoupon = await service.CreateAsync(options);
                    var mapperStripeToModel = _mapper.Map<CouponModel>(stripeCreateCoupon);
                    mapperStripeToModel.NameOfCoupon = model.NameOfCoupon;
                    mapperStripeToModel.StartDate = LocalDateTime.FromDateTime(model.StartDate ?? DateTime.UtcNow);
                    mapperStripeToModel.EndDate = LocalDateTime.FromDateTime(model.EndDate ?? DateTime.UtcNow);
                    mapperStripeToModel.Status = model.Status;
                    dataInsert = _mapper.Map<CouponEntities>(MappingCouponModel(mapperStripeToModel, model));
                }
                dataInsert = _mapper.Map<CouponEntities>(MappingCouponModel(new CouponModel(), model));

                return await _couponRepository.AddAsync(dataInsert);
            }
            catch (StripeException ex)
            {
                throw new ProjectException(ex.InnerException?.Message ?? string.Empty, ex);
            }
        }

        public async Task<bool> DeleteCouponForPromotion(string stripeCouponId)
        {
            try
            {
                var service = new CouponService();
                var deleteCouponStripe = await service.DeleteAsync(stripeCouponId);
                var getCouponOnData = await _couponRepository.GetObjectAsync(x => x.StripeCouponId == stripeCouponId);
                getCouponOnData.Status = (int)EStatus.Inactive;
                return await _couponRepository.UpdateAsync(getCouponOnData);
            }
            catch (StripeException ex)
            {
                throw new ProjectException(ex.InnerException?.Message ?? string.Empty, ex);
            }
        }

        public async Task<bool> InactivePromotionCode(string promotionCodeId)
        {
            try
            {
                var option = new PromotionCodeUpdateOptions()
                {
                    Active = false,
                };
                var service = new PromotionCodeService();
                var dataReturn = await service.UpdateAsync(promotionCodeId, option);
                return dataReturn.Id != string.Empty;
            }
            catch (StripeException ex)
            {
                throw new ProjectException(ex.InnerException?.Message ?? string.Empty, ex);
            }
        }

        public async Task<PagedResult<CouponModel>> GetListCouponActiveOnSystem(int pageIndex, int pageSize)
        {
            var getDataFromAPI = (await _couponRepository.GetAsync(x => x.Status != (int)EStatus.Inactive))
                .GetPagingPaged(pageIndex, pageSize, null);
            return _mapper.Map<PagedResult<CouponModel>>(getDataFromAPI);
        }

        public async Task<bool> UpdateCouponPromotionCode(CouponPromotionCode model)
        {
            var entitiesCoupon = await _couponRepository.GetByIdAsync(model.Id ?? 0);
            if (entitiesCoupon == null) { return false; }
            entitiesCoupon.StartDate = LocalDateTime.FromDateTime(model.StartDate ?? DateTime.UtcNow);
            entitiesCoupon.EndDate = LocalDateTime.FromDateTime(model.EndDate ?? DateTime.UtcNow);
            entitiesCoupon.Status = model.Status;
            entitiesCoupon.NameOfCoupon = model.NameOfCoupon;
            entitiesCoupon.PercentOff = model.PercentOff;

            if (model.Status == (int)EStatus.Active && entitiesCoupon.StripeCouponId == null)
            {
                return await ActiveCouponPromotion(entitiesCoupon);
            }
            else
            {
                return await _couponRepository.UpdateAsync(entitiesCoupon);
            }
        }

        public async Task<PagedResult<PromotionCodeModel>> GetListPromotions(int pageIndex, int pageSize)
        {
            var getDataFromAPI = (await _promotionCodeRepository.GetAsync(x => x.Status != (int)EStatus.Inactive))
                .GetPagingPaged(pageIndex, pageSize, null);
            return _mapper.Map<PagedResult<PromotionCodeModel>>(getDataFromAPI);

        }

        private async Task<bool> ActiveCouponPromotion(CouponEntities entities)
        {
            try
            {
                var options = new CouponCreateOptions
                {
                    Duration = entities.Duration,
                    PercentOff = entities?.PercentOff,
                    Currency = entities?.Currency
                };
                var service = new CouponService();
                var stripeCreateCoupon = await service.CreateAsync(options);
                var mapperStripeToModel = _mapper.Map<CouponEntities>(stripeCreateCoupon);
                mapperStripeToModel.Status = (int)EStatus.Active;
                mapperStripeToModel.Id = entities.Id;
                mapperStripeToModel.CreateDate = entities.CreateDate;
                mapperStripeToModel.NameOfCoupon = entities.NameOfCoupon;
                mapperStripeToModel.StartDate = entities.StartDate;
                mapperStripeToModel.EndDate = entities.EndDate;
                return await _couponRepository.UpdateAsync(mapperStripeToModel);
            }
            catch (StripeException ex)
            {
                throw new ProjectException(ex.InnerException?.Message ?? string.Empty, ex);
            }
        }

        private CouponModel MappingCouponModel(CouponModel modelReturn,  CouponPromotionCode model)
        {
            modelReturn.NameOfCoupon = model.NameOfCoupon;
            modelReturn.Duration = model.TypeDuration;
            modelReturn.Currency = model.Currency;
            modelReturn.PercentOff = model.PercentOff;
            modelReturn.StartDate = LocalDateTime.FromDateTime(model.StartDate ?? DateTime.UtcNow);
            modelReturn.EndDate = LocalDateTime.FromDateTime(model.EndDate ?? DateTime.UtcNow);
            modelReturn.Status = model.Status;
            return modelReturn;
        }
    }
}