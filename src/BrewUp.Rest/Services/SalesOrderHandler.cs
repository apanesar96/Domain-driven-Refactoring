using BrewUp.Mediator;
using BrewUp.Sales.Facade;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.Entities;
using BrewUp.Warehouses.Facade;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BrewUp.Rest.Services;

public static class SalesOrderHandler
{
    public static async Task<Results<Created, NotFound>> HandleCreateSalesOrder(WarehouseFacade warehouseFacade,
        SalesFacade salesFacade,
        SalesOrderJson body, CancellationToken cancellationToken)
    {
        await CreateSalesOrderMediator.CreateSalesOrder(warehouseFacade, salesFacade, body, cancellationToken);
        return TypedResults.Created($"v1/sales/{body.SalesOrderId}");
    }

    public static async Task<Results<Ok<PagedResult<SalesOrderJson>>, NotFound>> HandleGetOrders(
        ISalesQueryService salesQueryService, CancellationToken cancellationToken)
    {
        var orders = await salesQueryService.GetSalesOrdersAsync(0, 30, cancellationToken);
        return TypedResults.Ok(orders);
    }
}