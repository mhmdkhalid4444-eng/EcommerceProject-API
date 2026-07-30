using Ecommerce.Application.Common;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Services;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Payments
{
    internal class StripePaymentGateway : IPaymentGateway
    {
        private readonly PaymentIntentService _paymentIntentService = new();
        private readonly PaymentGatewaySettings _paymentGatewaySettings;

        public StripePaymentGateway(IOptions<PaymentGatewaySettings> options)
        {
            _paymentGatewaySettings = options.Value;
<<<<<<< HEAD
=======
            StripeConfiguration.ApiKey = _paymentGatewaySettings.SecretKey;
>>>>>>> master
        }
        public async Task<PaymentIntentResult> CreatePaymentIntentAsync( decimal amount,string currency,CancellationToken cancellationToken = default)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)amount,
                Currency = currency.ToLowerInvariant(),
                PaymentMethodTypes = ["card"]
            };

            var intent = await _paymentIntentService.CreateAsync(options, cancellationToken: cancellationToken);
            return new PaymentIntentResult(intent.Id, intent.ClientSecret);
        }

        public async Task<PaymentIntentResult> UpdatePaymentIntentAsync(
            string paymentIntentId,
            decimal amount,
            CancellationToken cancellationToken = default)
        {
            var options = new PaymentIntentUpdateOptions { Amount = (long)amount };
            var intent = await _paymentIntentService.UpdateAsync(paymentIntentId, options, cancellationToken: cancellationToken);
            return new PaymentIntentResult(intent.Id, intent.ClientSecret);
        }
    }
}
