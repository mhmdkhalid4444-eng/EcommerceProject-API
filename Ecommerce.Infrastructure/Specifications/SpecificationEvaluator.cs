using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Specifications
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {

            var query = inputQuery;

            if(specifications.Criteria != null)
            {
                query = query.Where(specifications.Criteria);
            }

            if (specifications.IncludeExpressions.Any())
            {
               

                query = specifications.IncludeExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            }

            if(specifications.OrderBy != null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            else if (specifications.OrderByDescending != null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }

            if(specifications.IsPagingEnabled)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
        

            return query;
        }
    }
}
