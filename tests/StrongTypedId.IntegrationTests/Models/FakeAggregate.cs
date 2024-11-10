namespace StrongTypedId.IntegrationTests.Models;

public record FakeAggregate(FakeId Id) : IAggregate<FakeId>;

public class FakeId : StrongTypedGuid<FakeId>
{
	public FakeId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}