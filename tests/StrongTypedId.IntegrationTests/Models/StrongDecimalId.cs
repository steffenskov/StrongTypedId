namespace StrongTypedId.IntegrationTests.Models;

public class StrongDecimalId : StrongTypedId<StrongDecimalId, decimal>
{
	public StrongDecimalId(decimal primitiveId) : base(primitiveId)
	{
	}
}