using BrewUp.Sales.Facade;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Queries;
using BrewUp.Warehouse.ReadModel.Dtos;
using BrewUp.Warehouses.Facade;

namespace BrewUp.Mediator;

public static class CreateSalesOrderMediator
{
    public static async Task CreateSalesOrder(WarehouseFacade warehouseFacade, SalesFacade salesFacade,
        SalesOrderJson body, CancellationToken cancellationToken, IQueries<AvailabilityDto> availabilityQueries)
    {
        var availableBeers = await AddAvailableBeers(body.Rows, warehouseFacade, cancellationToken, availabilityQueries);
        
        await salesFacade.CreateSalesOrderAsync(body, availableBeers, cancellationToken);
    }

    private static async Task<List<SalesOrderRowJson>> AddAvailableBeers(IEnumerable<SalesOrderRowJson> rows,
        WarehouseFacade warehouseFacade, CancellationToken cancellationToken,  
        IQueries<AvailabilityDto> availabilityQueries)
    {
        List<SalesOrderRowJson> beersAvailable = new();

        foreach (var row in rows)
        {
            var availability = await availabilityQueries.GetByIdAsync(row.BeerId.ToString(), cancellationToken);
            
            if (availability != null)
                beersAvailable.Add(row);
        }

        return beersAvailable;
    }
}