using BrewUp.Sales.Domain;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;

namespace BrewUp.Sales.Facade;

public class SalesFacade(ISalesOrderService salesOrderService)
{
    public async Task CreateSalesOrderAsync(SalesOrderJson salesOrder, List<SalesOrderRowJson> availableBeers,
        CancellationToken cancellationToken)
    {
        await salesOrderService.CreateSalesOrderAsync(
            new SalesOrderId(new Guid(salesOrder.SalesOrderId)),
            new SalesOrderNumber(salesOrder.SalesOrderNumber),
            new OrderDate(salesOrder.OrderDate),
            new CustomerId(salesOrder.CustomerId),
            new CustomerName(salesOrder.CustomerName),
            availableBeers,
            cancellationToken);
    }
}