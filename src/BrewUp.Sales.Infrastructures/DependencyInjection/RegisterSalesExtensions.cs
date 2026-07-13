using BrewUp.Sales.Domain;
using BrewUp.Sales.Facade;
using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.ReadModel.Queries;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.Domain;
using BrewUp.Shared.Entities;
using BrewUp.Shared.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Infrastructures.DependencyInjection;

public static class RegisterSalesExtensions
{
    public static void RegisterSalesMongoDb(this IServiceCollection services) => 
        services.AddKeyedScoped<IRepository, SalesRepository>("sales");

    public static IServiceCollection RegisterSales(this IServiceCollection services)
    {
        services.AddKeyedScoped<IRepository, SalesRepository>("sales");
        services.AddScoped<ISalesOrderService, SalesOrderService>();
        services.AddScoped<SalesFacade>();
        services.AddScoped<ISalesQueryService, SalesQueryService>();
        services.AddScoped<IQueries<SalesOrderDto>, SalesOrderQueries>();
        return services;
    }
}