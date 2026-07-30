using AutoMapper;
using E_Commerce.Domin.Contracts;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.DataSeeding;
using E_Commerce.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeding>("Cataloge");
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
