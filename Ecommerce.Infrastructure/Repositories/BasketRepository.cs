using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _connectionMultiplexer;

        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null, CancellationToken cancellationToken = default)
        {
            var value = JsonSerializer.Serialize(basket);
            var result = await _connectionMultiplexer.StringSetAsync(basket.Id, value, timeToLive ?? TimeSpan.FromDays(10));

            return result ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId, CancellationToken cancellationToken = default)
        {
           return await _connectionMultiplexer.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken cancellationToken = default)
        {
           var basket = await _connectionMultiplexer.StringGetAsync(basketId);

           return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }
    }
}
