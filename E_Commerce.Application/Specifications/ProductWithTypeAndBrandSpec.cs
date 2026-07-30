using E_Commerce.Application.Commen;
using E_Commerce.Domin.Entities.Products;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithTypeAndBrandSpec : BaseSpecifications<Product,int>
    {
        public ProductWithTypeAndBrandSpec(ProductQueryParams queryParams) 
            : base(P=> (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value) 
            && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())) )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p=> p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderDescBy(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderDescBy(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);

        }

        public ProductWithTypeAndBrandSpec(int id) : base(x=> x.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

    }
}
