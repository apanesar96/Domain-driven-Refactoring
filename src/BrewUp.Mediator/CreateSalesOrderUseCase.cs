using BrewUp.Sales.Domain;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Warehouses.Facade;

namespace BrewUp.Mediator;

public static class CreateSalesOrderUseCase
{
    public static async Task CreateSalesOrder(WarehouseFacade warehouseFacade, ISalesOrderService salesOrderService,
        SalesOrderJson body, CancellationToken cancellationToken)
    {
        var availableBeers = await AddAvailableBeers(body.Rows, warehouseFacade, cancellationToken);

        await salesOrderService.CreateSalesOrderAsync(
            new SalesOrderId(new Guid(body.SalesOrderId)),
            new SalesOrderNumber(body.SalesOrderNumber),
            new OrderDate(body.OrderDate),
            new CustomerId(body.CustomerId),
            new CustomerName(body.CustomerName),
            availableBeers,
            cancellationToken);
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