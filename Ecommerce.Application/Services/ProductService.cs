using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Specifications;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork  _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
            
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync(ct);

            var data = _mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(data);
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(queryParams);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec, ct);
            var totalCount = await _unitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecifications(queryParams), ct);
            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            var ressult = new PaginatedResult<ProductDto>(queryParams.PageIndex, queryParams.PageSize, totalCount, data);

            return Result<PaginatedResult<ProductDto>>.Ok(ressult);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct);

            var data = _mapper.Map<IReadOnlyList<TypeDto>>(types);
            return Result<IReadOnlyList<TypeDto>>.Ok(data);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec, ct);
            if (product == null)
                return Result<ProductDto>.Fail(Error.NotFound("product not found", $"product with id {id} not found"));

            var data = _mapper.Map<ProductDto>(product);
            return Result<ProductDto>.Ok(data);
        }
    }
}
