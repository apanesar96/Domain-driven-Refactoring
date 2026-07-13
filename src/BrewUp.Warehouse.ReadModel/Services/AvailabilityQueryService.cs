using BrewUp.Shared.Contracts;
using BrewUp.Shared.Entities;
using BrewUp.Shared.Queries;
using BrewUp.Warehouse.ReadModel.Dtos;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ReadModel.Services;

public sealed class AvailabilityQueryService(ILoggerFactory loggerFactory, IQueries<AvailabilityDto> queries) : ServiceBase(loggerFactory), IAvailabilityQueryService
{
	public async Task<PagedResult<BeerAvailabilityJson>> GetAvailabilityAsync(Guid beerId, CancellationToken cancellationToken)
	{
		try
		{
			var availability = await queries.GetByFilterAsync(a => a.BeerId.Equals(beerId.ToString()), 0, 20, cancellationToken);
			return availability.TotalRecords > 0
				? new PagedResult<BeerAvailabilityJson>(availability.Results.Select(r => r.ToJson()), availability.Page, availability.PageSize, availability.TotalRecords)
				: new PagedResult<BeerAvailabilityJson>(Enumerable.Empty<BeerAvailabilityJson>(), 0, 0, 0);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}