using Ecommerce.Application.Contracts;
using Ecommerce.Application.DTOs.Baskets;
using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ApiBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly PaymentGatewaySettings _stripeSettings;
        public PaymentController(IPaymentService paymentService, IOptions<PaymentGatewaySettings> options)
        {
            _paymentService = paymentService;
            _stripeSettings = options.Value;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId, CancellationToken cancellationToken)
            => ToActionResult(await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId, cancellationToken));



        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _stripeSettings.WebhookSecret);

                switch (stripeEvent.Type)
                {
                    case EventTypes.PaymentIntentSucceeded:

                        var succeededPaymentIntent = stripeEvent.Data.Object as PaymentIntent;

                        if (succeededPaymentIntent is not null)
                            await _paymentService.PaymentSucceeded(succeededPaymentIntent.Id);

                        break;

                    case EventTypes.PaymentIntentPaymentFailed:

                        var failedPaymentIntent = stripeEvent.Data.Object as PaymentIntent;

                        if (failedPaymentIntent is not null)
                            await _paymentService.PaymentFailed(failedPaymentIntent.Id);

                        break;

                    default:
                        break;
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
