using BrewUp.Shared.CustomTypes;
using Availability = BrewUp.DomainModel.Entities.Warehouses.Availability;

namespace BrewUp.DomainModel.Services;

public interface IWarehouseService
{
	Task UpdateAvailabilityDueToProductionOrderAsync(BeerId beerId, BeerName beerName, Quantity quantity,
		CancellationToken cancellationToken);
	
	Task<Availability> GetAvailabilityAsync(BeerId beerId, CancellationToken cancellationToken);
}