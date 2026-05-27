using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Domain;
using BrewUp.Warehouses.Domain.Helper;
using Microsoft.Extensions.DependencyInjection;
using Availability = BrewUp.Warehouses.Domain.Entities.Availability;

namespace BrewUp.Warehouses.Domain;

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