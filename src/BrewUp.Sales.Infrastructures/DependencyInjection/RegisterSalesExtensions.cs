using BrewUp.DomainModel.Services;
using BrewUp.Sales.Domain;
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
        return services;
    }
}