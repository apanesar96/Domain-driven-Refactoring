

using BrewUp.Shared.Entities;
using BrewUp.Warehouse.ReadModel.Dtos;

namespace BrewUp.Warehouses.Domain.Helper;

public static class DomainHelper
{
	internal static AvailabilityDto MapToSharedDto(this Entities.Availability availability) => 
		AvailabilityDto.Create(availability._beerId, availability._beerName, availability._quantity);
}