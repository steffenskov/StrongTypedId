namespace StrongTypedId.IntegrationTests.Repositories;

public interface IRepository<TAggregate, in TAggregateId>
{
	public Task InsertAsync(TAggregate aggregate);

	public Task<TAggregate?> GetAsync(TAggregateId aggregateId);
}