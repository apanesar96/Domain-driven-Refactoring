using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Entities;

namespace BrewUp.Sales.ReadModel.Dtos;

public class AvailabilityDto : EntityBase
{
    public string BeerId { get; private set; } = string.Empty;
    public string BeerName { get; private set; } = string.Empty;

    public Quantity Quantity { get; private set; } = new(0, string.Empty);

    protected AvailabilityDto()
    {
    }

    public static AvailabilityDto Create(BeerId beerId, BeerName beerName, Quantity quantity)
    {
        return new AvailabilityDto(beerId.Value.ToString(), beerName.Value, quantity);
    }

    private AvailabilityDto(string beerId, string beerName, Quantity quantity)
    {
        Id = beerId;

        BeerId = beerId;
        BeerName = beerName;
        Quantity = quantity;
    }

    public BeerAvailabilityJson ToJson() => new(Id, BeerName,
        new Shared.CustomTypes.Availability(0, Quantity.Value, Quantity.UnitOfMeasure));
}