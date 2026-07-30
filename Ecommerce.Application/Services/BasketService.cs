using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.DTOs.Baskets;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class BasketService : IBasketservice
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, TimeSpan? TLV = null , CancellationToken cancellationToken = default)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);

            var basketresult = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket,TLV  , cancellationToken);

            return basketresult == null ? Result<BasketDto>.Fail(Error.Failure("Failed to create or update basket")) 
                : Result<BasketDto>.Ok(_mapper.Map<BasketDto>(basketresult));
        }

        public async Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = await _basketRepository.DeleteBasketAsync(id, cancellationToken);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("Failed to delete basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken cancellationToken = default)
        {
          var basket = await _basketRepository.GetBasketAsync(id, cancellationToken);
          return basket == null ? Result<BasketDto>.Fail(Error.Failure("Basket not found")) 
              : Result<BasketDto>.Ok(_mapper.Map<BasketDto>(basket));
        }
    }
}
