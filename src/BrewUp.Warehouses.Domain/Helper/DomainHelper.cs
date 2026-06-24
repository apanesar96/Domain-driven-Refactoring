

using BrewUp.Shared.Entities;

namespace BrewUp.Warehouses.Domain.Helper;

public static class DomainHelper
{
	internal static Availability MapToSharedDto(this Entities.Availability availability) => 
		Availability.Create(availability._beerId, availability._beerName, availability._quantity);
}