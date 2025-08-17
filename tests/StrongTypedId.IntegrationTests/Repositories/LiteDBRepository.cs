using LiteDB;

namespace StrongTypedId.IntegrationTests.Repositories;

public interface ILiteDBRepository<TAggregate, in TAggregateId>
{
	void Insert(TAggregate aggregate);
	IEnumerable<TAggregate> GetAll();
	TAggregate? GetSingle(TAggregateId aggregateId);
}

public class LiteDBRepository<TAggregate, TAggregateId> : ILiteDBRepository<TAggregate, TAggregateId>
	where TAggregate : IAggregate<TAggregateId>
	where TAggregateId : StrongTypedGuid<TAggregateId>
{
	private readonly ILiteCollection<TAggregate> _collection;

	public LiteDBRepository(LiteDatabase db, string collectionName)
	{
		_collection = db.GetCollection<TAggregate>(collectionName);
	}

	public void Insert(TAggregate aggregate)
	{
		_collection.Insert(aggregate);
	}

	public IEnumerable<TAggregate> GetAll()
	{
		return _collection.FindAll();
	}

	public TAggregate? GetSingle(TAggregateId aggregateId)
	{
		return _collection.FindAll().FirstOrDefault(e => e.Id == aggregateId);
		//return _collection.FindById(aggregateId.PrimitiveValue);
	}
}