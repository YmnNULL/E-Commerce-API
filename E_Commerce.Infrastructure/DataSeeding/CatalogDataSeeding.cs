using E_Commerce.Domin.Commen;
using E_Commerce.Domin.Contracts;
using E_Commerce.Domin.Entities.Products;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace E_Commerce.Infrastructure.DataSeeding
{
    internal class CatalogDataSeeding(StoreDbContext dbContext , ILogger<CatalogDataSeeding> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PendingMigration = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (PendingMigration.Any())
                    await dbContext.Database.MigrateAsync(ct);

                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                await SeedIfEmpty<Product, int>(seedRoot, "products.json", ct);
                await SeedIfEmpty<ProductBrand, int>(seedRoot, "brands.json", ct);
                await SeedIfEmpty<ProductType, int>(seedRoot, "types.json", ct);

                int result = await dbContext.SaveChangesAsync(ct);

                if (result > 0)
                    logger.LogInformation($"{result} Rows Added");
                else
                    logger.LogInformation("Database Already Seeded");

            }
            catch
            {

            }
        }
    
    
        private async Task SeedIfEmpty<T , Tkey>(string rootPath, string fileName , CancellationToken ct = default) where T : BaseEntity<Tkey> 
        {
            if (await dbContext.Set<T>().AnyAsync())
            {
                logger.LogInformation("Table Already Has Data");
                return;
            }

            var filePath = Path.Combine(rootPath, fileName);

            if(!File.Exists(filePath))
            {
                logger.LogWarning($"File {fileName} Is Not Exist");
                return;
            }


            using var fileStream = File.OpenRead(filePath);
            var opt = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, opt, ct);

            if (items?.Any() ?? false)
                dbContext.Set<T>().AddRange(items);
        }
    }
}
