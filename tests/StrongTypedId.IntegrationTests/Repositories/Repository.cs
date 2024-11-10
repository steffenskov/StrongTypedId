using MongoDB.Driver;

namespace StrongTypedId.IntegrationTests.Repositories;

internal class Repository<TAggregate, TAggregateId> : IRepository<TAggregate, TAggregateId>
	where TAggregate : IAggregate<TAggregateId>
	where TAggregateId : StrongTypedGuid<TAggregateId>
{
	private readonly IMongoCollection<TAggregate> _collection;

	public Repository(IMongoDatabase database, string collectionName)
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