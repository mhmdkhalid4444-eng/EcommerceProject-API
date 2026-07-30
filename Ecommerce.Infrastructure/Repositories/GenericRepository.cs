using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity , Tkey>(StoreDbContext dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
   
        public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default) 
            => await dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default)
        {
           var query = SpecificationEvaluator.GetQuery(dbContext.Set<TEntity>(), specifications);

            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default)
            => await dbContext.Set<TEntity>().FindAsync(id, ct);

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default)
        {
          var query= SpecificationEvaluator.GetQuery(dbContext.Set<TEntity>(), specifications);
            return await query.FirstOrDefaultAsync();
        }

        public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);


        public Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken cancellationToken = default)
         => SpecificationEvaluator.GetQuery(dbContext.Set<TEntity>(), specifications).CountAsync(cancellationToken);
      

        }
    }
