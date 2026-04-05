using BrewUp.DomainModel.Services;
using BrewUp.Sales.Domain.Entities;
using BrewUp.Sales.Domain.Helper;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Domain;

public sealed class SalesOrderService(
	[FromKeyedServices("sale")] IRepository saleRepository,
	[FromKeyedServices("warehouse")] IRepository warehouseRepository) : ISalesOrderService
{
	public async Task CreateSalesOrderAsync(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, IEnumerable<SalesOrderRowJson> rows, CancellationToken cancellationToken)
	{
		List<SalesOrderRowJson> beersAvailable = new();
		foreach (var row in rows)
		{
			var availability = await warehouseRepository.GetByIdAsync<Shared.Entities.Availability>(row.BeerId.ToString(), cancellationToken);
			if (availability != null)
				beersAvailable.Add(row);
		}

		var aggregate = SalesOrder.CreateSalesOrder(salesOrderId, salesOrderNumber, orderDate, customerId, customerName, beersAvailable);

		await saleRepository.InsertAsync(aggregate.MapToSharedDto(), cancellationToken);
	}
}