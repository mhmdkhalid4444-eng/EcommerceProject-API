using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Common
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? SearchValue { get; set; }

        public ProductSortingOptions Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        private int pagesize = DefaultPageSize;
        private const int maxPageSize = 10;
        private const int DefaultPageSize = 10;
        public int PageSize
        {
            get => pagesize;
            set => pagesize = value > maxPageSize ? maxPageSize : (value < 1 ? DefaultPageSize : value);
        }


    }
}
