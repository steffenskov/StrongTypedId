namespace StrongTypedId.IntegrationTests.Models;

public interface IAggregate<out TAggregateId>
{
	TAggregateId Id { get; }
}