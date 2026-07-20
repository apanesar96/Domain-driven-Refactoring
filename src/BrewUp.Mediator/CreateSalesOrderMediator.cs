using BrewUp.Sales.Facade;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;

namespace BrewUp.Mediator;

public static class CreateSalesOrderMediator
{
    public static async Task CreateSalesOrder(SalesFacade salesFacade,
        SalesOrderJson body, CancellationToken cancellationToken)
    {
        await salesFacade.CreateSalesOrderAsync(body, cancellationToken);
    }
}