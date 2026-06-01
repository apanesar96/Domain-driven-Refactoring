using BrewUp.Sales.Facade;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Warehouses.Facade;

namespace BrewUp.Mediator;

public static class CreateSalesOrderMediator
{
    public static async Task CreateSalesOrder(WarehouseFacade warehouseFacade, SalesFacade salesFacade,
        SalesOrderJson body, CancellationToken cancellationToken)
    {
        var availableBeers = await AddAvailableBeers(body.Rows, warehouseFacade, cancellationToken);
        
        await salesFacade.CreateSalesOrderAsync(body, availableBeers, cancellationToken);
    }

    private static async Task<List<SalesOrderRowJson>> AddAvailableBeers(IEnumerable<SalesOrderRowJson> rows,
        WarehouseFacade warehouseFacade, CancellationToken cancellationToken)
    {
        List<SalesOrderRowJson> beersAvailable = new();

        foreach (var row in rows)
        {
            var availability =
                await warehouseFacade.GetBeerAvailabilityAsync(new BeerId(row.BeerId), cancellationToken);

            if (availability != null)
                beersAvailable.Add(row);
        }

        return beersAvailable;
    }
}