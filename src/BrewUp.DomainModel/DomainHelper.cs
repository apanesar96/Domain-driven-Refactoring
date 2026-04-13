namespace BrewUp.DomainModel;

public static class DomainHelper
{
	internal static Shared.Entities.Availability MapToSharedDto(this Entities.Warehouses.Availability availability)
	{
		return Shared.Entities.Availability.Create(availability._beerId, availability._beerName, availability._quantity);
	}
}