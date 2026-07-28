using E_Commerce.Domin.Commen;

namespace E_Commerce.Domin.Entities.Products
{
    public class ProductType :BaseEntity<int>
    {
        public string Name { get; set; } = default!;

    }
}
