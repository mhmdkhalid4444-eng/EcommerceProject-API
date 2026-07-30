using Ecommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _connection;

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _connection = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string cacheKey, CancellationToken ct = default)
        {
            var value = await _connection.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ? null : value.ToString();

        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan? timeToLive, CancellationToken cancellationToken = default)
        {
          await  _connection.StringSetAsync(cacheKey, cacheValue, timeToLive ?? TimeSpan.FromDays(2));

        }   
    }
}
