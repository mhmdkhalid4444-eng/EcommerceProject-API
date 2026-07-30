using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var typename = typeof(TEntity).Name;
            if(repositories.TryGetValue(typename, out object? value))
                return (IGenericRepository<TEntity, Tkey>)value;
            else
            {
                var repo = new GenericRepository<TEntity, Tkey>(dbContext);
                repositories[typename] = repo;
                return repo;
            }
        
        }

        public async Task<int> SaveChangeAsync(CancellationToken ct = default)
            => await dbContext.SaveChangesAsync(ct);

    }
}
