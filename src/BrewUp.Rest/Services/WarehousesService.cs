using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Warehouses.Facade;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BrewUp.Rest.Services
{
    public static class WarehousesService
    {
        public static async Task<Ok> HandleSetAvailabilities(SetAvailabilityJson body,
            WarehouseFacade warehouseFacade, CancellationToken cancellationToken)
        {
            await warehouseFacade.UpdateAvailabilityDueToProductionOrderAsync(new BeerId(new Guid(body.BeerId)),
                new BeerName(body.BeerName), body.Quantity, cancellationToken);
            
            return TypedResults.Ok();
        }
    }
}