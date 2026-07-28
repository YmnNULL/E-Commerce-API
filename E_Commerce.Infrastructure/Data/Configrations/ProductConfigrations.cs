using E_Commerce.Domin.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Data.Configrations
{
    internal class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.ProductBrand)
                .WithMany()
                .HasForeignKey(x => x.BrandId);
            builder.HasOne(x => x.ProductType)
                .WithMany()
                .HasForeignKey(x => x.TypeId);

            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.PictureUrl).HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(500);
        }
    }
}
