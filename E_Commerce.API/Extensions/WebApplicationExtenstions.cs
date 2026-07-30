using E_Commerce.Domin.Contracts;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationExtenstions
    {
        public static async Task<WebApplication> SeedAndMigrateDataAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Cataloge");

            await seeder.SeedDataAsync();
            return app;
        }
    }
}
