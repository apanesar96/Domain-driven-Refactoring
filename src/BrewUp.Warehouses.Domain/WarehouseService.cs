using BrewUp.Shared.CustomTypes;
using Microsoft.Extensions.DependencyInjection;
using Availability = BrewUp.DomainModel.Entities.Warehouses.Availability;

namespace BrewUp.DomainModel.Services;

public sealed class WarehouseService([FromKeyedServices("warehouse")] IRepository repository) : IWarehouseService
{
	public async Task UpdateAvailabilityDueToProductionOrderAsync(BeerId beerId, BeerName beerName, Quantity quantity,
		CancellationToken cancellationToken)
	{
		var aggregate = Availability.CreateAvailability(beerId, beerName, quantity);
		await repository.InsertAsync(aggregate.MapToSharedDto(), cancellationToken);
	}

	public async Task<Availability> GetAvailabilityAsync(BeerId beerId, CancellationToken cancellationToken) => 
		await repository.GetByIdAsync<Availability>(beerId.ToString(), cancellationToken);
}