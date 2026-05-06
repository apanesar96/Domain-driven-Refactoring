using BrewUp.DomainModel.Services;
using BrewUp.Sales.Domain.Entities;
using BrewUp.Sales.Domain.Helper;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Domain;

public sealed class SalesOrderService(
	[FromKeyedServices("sales")] IRepository salesRepository) : ISalesOrderService
{
	public async Task CreateSalesOrderAsync(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, IEnumerable<SalesOrderRowJson> rows, CancellationToken cancellationToken)
	{
		var aggregate = SalesOrder.CreateSalesOrder(salesOrderId, salesOrderNumber, orderDate, customerId, customerName, rows);

		await salesRepository.InsertAsync(aggregate.MapToSharedDto(), cancellationToken);
	}
}