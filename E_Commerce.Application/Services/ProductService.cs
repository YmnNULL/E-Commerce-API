using AutoMapper;
using E_Commerce.Application.Commen;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Specifications;
using E_Commerce.Domin.Contracts;
using E_Commerce.Domin.Entities.Products;

namespace E_Commerce.Application.Services
{
    public class ProductService : IProductsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct = default)
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);
            var data = mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(data);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProducts(ProductQueryParams? queryParams, CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(queryParams);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            return Result<IReadOnlyList<ProductDto>>.Ok(mapper.Map<IReadOnlyList<ProductDto>>(products));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct = default)
        {
            var types = mapper.Map<IReadOnlyList<TypeDto>>
                (await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct));
            return Result<IReadOnlyList<TypeDto>>.Ok(types);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id,CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec,ct);

            if (product == null)
                return Error.NotFound("Product.NotFound", $"Product With Id {id} Not Found");

            return mapper.Map<ProductDto>(product);
        }
    }
}
