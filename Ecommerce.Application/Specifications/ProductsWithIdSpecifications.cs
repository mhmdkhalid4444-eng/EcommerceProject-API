using Ecommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Specifications
{
    internal class ProductsWithIdSpecifications : BaseSpecification<Product, int>
    {
        public ProductsWithIdSpecifications(HashSet<int> productIds) : base(p => productIds.Contains(p.Id))
        {

        }
    }
}
