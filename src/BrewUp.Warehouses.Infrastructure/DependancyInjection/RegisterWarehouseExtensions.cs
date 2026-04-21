using BrewUp.DomainModel.Services;
using BrewUp.Infrastructure.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouses.Infrastructure.DependancyInjection;

public static class RegisterWarehouseExtensions
{
    public static IServiceCollection RegisterWarehouse(this IServiceCollection services)
    {
        services.AddKeyedScoped<IRepository, WarehouseRepository>("warehouse");
        services.AddScoped<IWarehouseService, WarehouseService>();
        return services;
    }
}