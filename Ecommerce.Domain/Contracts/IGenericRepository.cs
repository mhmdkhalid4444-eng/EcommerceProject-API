using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        Task<TEntity?> GetByIdAsync(Tkey id , CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync( CancellationToken ct = default);
        Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken cancellationToken = default);



    }
}
