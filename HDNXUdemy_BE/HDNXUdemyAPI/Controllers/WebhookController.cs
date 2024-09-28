using Asp.Versioning;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace HDNXUdemyAPI.Controllers
{
    /// <summary>
    /// WebhookController
    /// </summary>
    [ApiVersion(VersionApi.Version1)]
    [Route(RouterControllerName.WebHook)]
    public class WebhookController : BaseController
    {
        private readonly IPurcharseCourseServices _purcharseCourseServices;

        /// <summary>
        /// WebhookController
        /// </summary>
        public WebhookController(IPurcharseCourseServices purcharseCourseServices)
        {
            _purcharseCourseServices = purcharseCourseServices ?? throw new ProjectException(nameof(_purcharseCourseServices));
        }

        /// <summary>
        /// StripeCheckOutSessionHook
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ProjectException"></exception>
        [HttpPost("checkout-session-hook")]
        public async Task<IActionResult> StripeCheckOutSessionHook()
        {
            var dataJson = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    dataJson,
                    HttpContext.Request.Headers["Stripe-Signature"],
                    ProjectConfig.StripeWebHookKey,
                    throwOnApiVersionMismatch: false
                    );
                if (stripeEvent.Data.Object != null)
                {
                    Session session = stripeEvent.Data.Object as Session ?? new Session();
                    await _purcharseCourseServices.CreateAndUpdatePurchaseOrderWhenPaymentFromStripe(session, stripeEvent);
                    return Ok();
                }
                else { return Ok(false); }
            }
            catch (StripeException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ProjectException(ex.InnerException?.Message ?? string.Empty, ex);
            }
        }
    }
}