using BrewUp.DomainModel.Services;
using BrewUp.Shared.CustomTypes;
using Availability = BrewUp.DomainModel.Entities.Warehouses.Availability;

namespace BrewUp.Warehouses.Facade;

public class WarehouseFacade(IWarehouseService warehouseService) 
{
    public async Task<Availability> GetBeerAvailabilityAsync(BeerId beerId, CancellationToken cancellationToken = default) => 
        await warehouseService.GetAvailabilityAsync(beerId, CancellationToken.None);
}