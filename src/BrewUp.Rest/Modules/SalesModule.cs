using BrewUp.Sales.Infrastructures.DependencyInjection;

namespace BrewUp.Rest.Modules;

public class SalesModule : IModule
{
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        return builder.Services.RegisterSales();
    }
}