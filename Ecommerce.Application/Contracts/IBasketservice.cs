using Ecommerce.Application.Common;
using Ecommerce.Application.DTOs.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts
{
    public interface IBasketservice
    {
        Task<Result<BasketDto>> GetBasketAsync(string id,  CancellationToken cancellationToken = default);
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, TimeSpan? TLV = null, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken cancellationToken = default);
    }
}
  