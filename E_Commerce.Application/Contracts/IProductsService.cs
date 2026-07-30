using E_Commerce.Application.Commen;
using E_Commerce.Application.DTOs.Products;

namespace E_Commerce.Application.Contracts
{
    public interface IProductsService
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProducts(ProductQueryParams? queryParams ,CancellationToken ct = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct = default);
        Task<Result<ProductDto>> GetProductByIdAsync(int id ,CancellationToken ct = default);

    }
}
