using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync(CancellationToken ct = default);

        IGenericRepository<TEntity ,Tkey> GetRepository<TEntity ,Tkey>() where TEntity : BaseEntity<Tkey>;
    }
}
