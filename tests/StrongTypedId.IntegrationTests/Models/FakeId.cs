namespace StrongTypedId.IntegrationTests.Models;

public class FakeId : StrongTypedGuid<FakeId>
{
	public FakeId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}