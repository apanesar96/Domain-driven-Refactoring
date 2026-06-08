using BrewUp.Shared.CustomTypes;
using BrewUp.Warehouses.Domain;
using Availability = BrewUp.Warehouses.Domain.Entities.Availability;

namespace BrewUp.Warehouses.Facade;

public class WarehouseFacade(IWarehouseService warehouseService)
{
    public async Task<Availability> GetBeerAvailabilityAsync(BeerId beerId,
        CancellationToken cancellationToken = default) =>
        await warehouseService.GetAvailabilityAsync(beerId, CancellationToken.None);

    public async Task UpdateAvailabilityDueToProductionOrderAsync(BeerId beerId, BeerName beerName, Quantity quantity,
        CancellationToken cancellationToken) =>
        await warehouseService.UpdateAvailabilityDueToProductionOrderAsync(beerId, beerName, quantity,
            cancellationToken);
}