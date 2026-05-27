using BrewUp.Warehouses.Domain.Entities;

namespace BrewUp.Warehouses.Domain.Helper;

public static class DomainHelper
{
	internal static Shared.Entities.Availability MapToSharedDto(this Availability availability)
	{
		return Shared.Entities.Availability.Create(availability._beerId, availability._beerName, availability._quantity);
	}
}