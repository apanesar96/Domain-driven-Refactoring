using BrewUp.Sales.Domain;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Entities;
using BrewUp.Warehouses.Facade;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BrewUp.Rest.Services;

public static class SalesOrderApplicationService
{
	public static async Task<Results<Created, NotFound>> HandleCreateSalesOrder(WarehouseFacade warehouseFacade,
		ISalesOrderService salesOrderService, 
		SalesOrderJson body, CancellationToken cancellationToken)
	{
		var availableSalesOrder = await AddAvailableBeers(body.Rows, warehouseFacade, cancellationToken);
		await salesOrderService.CreateSalesOrderAsync(new SalesOrderId(new Guid(body.SalesOrderId)),
			new SalesOrderNumber(body.SalesOrderNumber), new OrderDate(body.OrderDate),
			new CustomerId(body.CustomerId), new CustomerName(body.CustomerName),
			availableSalesOrder, cancellationToken);

		return TypedResults.Created($"v1/sales/{body.SalesOrderId}");
	}

	public static async Task<Results<Ok<PagedResult<SalesOrderJson>>, NotFound>> HandleGetOrders(ISalesQueryService salesQueryService, CancellationToken cancellationToken)
	{
		var orders = await salesQueryService.GetSalesOrdersAsync(0, 30, cancellationToken);
		return TypedResults.Ok(orders);
	}
	
	private static async Task<List<SalesOrderRowJson>> AddAvailableBeers(IEnumerable<SalesOrderRowJson> rows, WarehouseFacade warehouseFacade, CancellationToken cancellationToken)
	{
		List<SalesOrderRowJson> beersAvailable = new();
		foreach (var row in rows)
		{
			var availability = await warehouseFacade.GetBeerAvailabilityAsync(new BeerId(row.BeerId), cancellationToken);
			if (availability != null)
				beersAvailable.Add(row);
		}

		return beersAvailable;
	}
}