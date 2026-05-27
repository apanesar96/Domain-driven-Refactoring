using BrewUp.Shared.CustomTypes;
using Availability = BrewUp.Warehouses.Domain.Entities.Availability;

namespace BrewUp.Warehouses.Domain;

public interface IWarehouseService
{
	Task UpdateAvailabilityDueToProductionOrderAsync(BeerId beerId, BeerName beerName, Quantity quantity,
		CancellationToken cancellationToken);
	
	Task<Availability> GetAvailabilityAsync(BeerId beerId, CancellationToken cancellationToken);
}