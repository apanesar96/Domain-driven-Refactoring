using BrewUp.Sales.Domain.Entities;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;

namespace BrewUp.Sales.Domain.Helper;

public static class DomainHelper
{
    internal static IEnumerable<SalesOrderRow> MapToDomainRows(this IEnumerable<SalesOrderRowJson> json)
    {
        return json.Select(r => SalesOrderRow.CreateSalesOrderRow(new BeerId(r.BeerId), new BeerName(r.BeerName), r.Quantity, r.Price));
    }

    internal static BrewUp.Sales.ReadModel.Dtos.SalesOrder MapToSharedDto(this SalesOrder salesOrder)
    {
        return BrewUp.Sales.ReadModel.Dtos.SalesOrder.Create(salesOrder._salesOrderId, salesOrder._salesOrderNumber,
            salesOrder._orderDate, salesOrder._customerId, salesOrder._customerName,
            salesOrder._rows.Select(r => new SalesOrderRowJson
            {
                BeerId = r._beerId.Value,
                BeerName = r._beerName.Value,
                Quantity = r._quantity,
                Price = r._beerPrice
            }));    
    }
}