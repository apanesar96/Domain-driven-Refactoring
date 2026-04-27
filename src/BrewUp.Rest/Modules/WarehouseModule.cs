using BrewUp.Warehouses.Infrastructure.DependancyInjection;

namespace BrewUp.Rest.Modules;

public class WarehouseModule : IModule
{
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        return builder.Services.RegisterWarehouse();
    }
}