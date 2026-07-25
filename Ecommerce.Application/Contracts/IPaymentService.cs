using Ecommerce.Application.Common;
using Ecommerce.Application.DTOs.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts
{
    public interface IPaymentService
    {
        Task<Result<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId, CancellationToken cancellationToken = default);
        Task PaymentSucceeded(string paymentIntentId);
        Task PaymentFailed(string paymentIntentId);
    }
}
