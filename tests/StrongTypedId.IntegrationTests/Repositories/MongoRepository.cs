using MongoDB.Driver;

namespace StrongTypedId.IntegrationTests.Repositories;

public interface IMongoRepository<TAggregate, in TAggregateId>
{
	Task InsertAsync(TAggregate aggregate);
	Task<TAggregate?> GetAsync(TAggregateId aggregateId);
}

internal class MongoRepository<TAggregate, TAggregateId> : IMongoRepository<TAggregate, TAggregateId>
	where TAggregate : IAggregate<TAggregateId>
	where TAggregateId : StrongTypedGuid<TAggregateId>
{
	private readonly IMongoCollection<TAggregate> _collection;

	public MongoRepository(IMongoDatabase database, string collectionName)
	{
		_collection = database.GetCollection<TAggregate>(collectionName);
	}

	public async Task InsertAsync(TAggregate aggregate)
	{
		await _collection.InsertOneAsync(aggregate);
	}

	public async Task<TAggregate?> GetAsync(TAggregateId aggregateId)
	{
		var result = _collection.Find(x => x.Id == aggregateId);

		return await result.SingleOrDefaultAsync();
	}
}