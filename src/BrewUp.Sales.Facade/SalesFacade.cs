using BrewUp.Sales.Domain;
using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Queries;


namespace BrewUp.Sales.Facade;

public class SalesFacade(ISalesOrderService salesOrderService, IQueries<AvailabilityDto> availabilityQueries)
{
    public async Task CreateSalesOrderAsync(SalesOrderJson salesOrder, CancellationToken cancellationToken)
    {
        var availableBeers =
            await AddAvailableBeers(salesOrder.Rows, availabilityQueries, cancellationToken);
        
        await salesOrderService.CreateSalesOrderAsync(
            new SalesOrderId(new Guid(salesOrder.SalesOrderId)),
            new SalesOrderNumber(salesOrder.SalesOrderNumber),
            new OrderDate(salesOrder.OrderDate),
            new CustomerId(salesOrder.CustomerId),
            new CustomerName(salesOrder.CustomerName),
            availableBeers,
            cancellationToken);
    }
    
    private static async Task<List<SalesOrderRowJson>> AddAvailableBeers(IEnumerable<SalesOrderRowJson> rows, IQueries<AvailabilityDto> availabilityQueries, CancellationToken cancellationToken)
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