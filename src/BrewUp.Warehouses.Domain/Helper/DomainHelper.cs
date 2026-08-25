using BrewUp.Warehouses.Domain.Entities;

namespace BrewUp.Warehouses.Domain.Helper;

public static class DomainHelper
{
	internal static Warehouse.ReadModel.Dtos.Availability MapToSharedDto(this Availability availability)
	{
		return Warehouse.ReadModel.Dtos.Availability.Create(availability._beerId, availability._beerName, availability._quantity);
	}
}