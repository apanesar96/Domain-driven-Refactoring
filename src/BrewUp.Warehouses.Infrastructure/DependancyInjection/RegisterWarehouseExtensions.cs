using BrewUp.Shared.Domain;
using BrewUp.Shared.Entities;
using BrewUp.Shared.Queries;
using BrewUp.Warehouse.ReadModel.Dtos;
using BrewUp.Warehouse.ReadModel.Queries;
using BrewUp.Warehouse.ReadModel.Services;
using BrewUp.Warehouses.Domain;
using BrewUp.Warehouses.Facade;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouses.Infrastructure.DependancyInjection;

public static class RegisterWarehouseExtensions
{
    public static IServiceCollection RegisterWarehouse(this IServiceCollection services)
    {
        services.AddKeyedScoped<IRepository, WarehouseRepository>("warehouse");
        services.AddScoped<IWarehouseService, WarehouseDomainService>();
        services.AddScoped<WarehouseFacade>();

        services.AddScoped<IAvailabilityQueryService, AvailabilityQueryService>();
        services.AddScoped<IQueries<Availability>, AvailabilityQueries>();
        return services;
    }
}