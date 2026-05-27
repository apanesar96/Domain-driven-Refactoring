using BrewUp.DomainModel.Services;
using BrewUp.Infrastructure.MongoDb;
using BrewUp.Warehouses.Facade;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouses.Infrastructure.DependancyInjection;

public static class RegisterWarehouseExtensions
{
    public static IServiceCollection RegisterWarehouse(this IServiceCollection services)
    {
        services.AddKeyedScoped<IRepository, WarehouseRepository>("warehouse");
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<WarehouseFacade>();
        return services;
    }
}