using System.Linq.Expressions;
using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Entities;
using BrewUp.Shared.Queries;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BrewUp.Sales.ReadModel.Queries;

public sealed class AvailabilityQueries(IMongoClient mongoClient) : IQueries<AvailabilityDto>
{
	private readonly IMongoDatabase _database = mongoClient.GetDatabase("Sales");

	public async Task<AvailabilityDto> GetByIdAsync(string id, CancellationToken cancellationToken)
	{
		var collection = _database.GetCollection<AvailabilityDto>(nameof(AvailabilityDto));
		var filter = Builders<AvailabilityDto>.Filter.Eq("_id", id);
		return (await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0
			? (await collection.FindAsync(filter, cancellationToken: cancellationToken).ConfigureAwait(false)).First()
			: null)!;
	}

	public async Task<PagedResult<AvailabilityDto>> GetByFilterAsync(Expression<Func<AvailabilityDto, bool>>? query, int page, int pageSize, CancellationToken cancellationToken)
	{
		if (--page < 0)
			page = 0;

		var collection = _database.GetCollection<AvailabilityDto>(nameof(AvailabilityDto));
		var queryable = query != null
			? collection.AsQueryable().Where(query)
			: collection.AsQueryable();

		var count = await queryable.CountAsync(cancellationToken: cancellationToken);
		var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

		return new PagedResult<AvailabilityDto>(results, page, pageSize, count);
	}
}